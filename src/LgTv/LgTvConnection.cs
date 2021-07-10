using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LgTv.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace LgTv
{
    public class LgTvConnection : ILgTvConnection
    {
        private const int BufferSize = 4096;

        private readonly ConcurrentDictionary<string, TaskCompletionSource<dynamic>> _tokens = new ConcurrentDictionary<string, TaskCompletionSource<dynamic>>();

        private ClientWebSocket _socket;
        private int _commandCount;

        public event IsConnectedDelegate IsConnected;

        public async Task<bool> Connect(Uri uri)
        {
            _commandCount = 0;
            _socket = new ClientWebSocket();

            await _socket.ConnectAsync(uri, CancellationToken.None);

            IsConnected?.Invoke(true);

            _ = Run();

            return true;
        }

        private async Task Run()
        {
            var bufferBytes = new byte[BufferSize];
            var bufferSegment = new ArraySegment<byte>(bufferBytes);

            var messageStream = new MemoryStream();
            var messageLength = 0;
            while (true) // TODO Stop loop when connection is disposed
            {
                var receiveResult = await _socket.ReceiveAsync(bufferSegment, CancellationToken.None);

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }

                await messageStream.WriteAsync(bufferBytes, 0, receiveResult.Count);
                messageLength += receiveResult.Count;

                if (!receiveResult.EndOfMessage)
                {
                    continue;
                }

                var messageBytes = new byte[messageLength];

                messageStream.Position = 0;
                await messageStream.ReadAsync(messageBytes, 0, messageLength);

                messageLength = 0;
                Array.Clear(bufferBytes, 0, BufferSize);
                messageStream.Dispose();
                messageStream = new MemoryStream();

                var message = Encoding.UTF8.GetString(messageBytes);

                ProcessMessage(message);
            }

            messageStream?.Dispose();
            messageStream = null;
        }


        private void ProcessMessage(string message)
        {
            var obj = JsonConvert.DeserializeObject<dynamic>(message);
            var id = (string)obj.id;
            var type = (string)obj.type;

            TaskCompletionSource<dynamic> taskCompletion;
            if (type == "registered")
            {
                if (_tokens.TryRemove(id, out taskCompletion))
                {
                    var key = (string)JObject.Parse(message)["payload"]["client-key"];
                    taskCompletion.TrySetResult(new { clientKey = key });
                }
            }
            else
            if (_tokens.TryGetValue(id, out taskCompletion))
            {
                if (id == "register_0")
                {
                    return;
                }

                if (obj.type == "error")
                {
                    taskCompletion.SetException(new Exception(obj.error));
                }
                //else if (args.Cancelled)
                //{
                //    taskSource.SetCanceled();
                //}
                taskCompletion.TrySetResult(obj.payload);
            }
        }


        public async Task SendMessageAsync(string message)
        {
            await _socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public Task<dynamic> SendCommandAsync(string message)
        {
            var obj = JsonConvert.DeserializeObject<dynamic>(message);
            return SendCommandAsync((string)obj.id, message);
        }

        private Task<dynamic> SendCommandAsync(string id, string message)
        {
            try
            {
                var taskSource = new TaskCompletionSource<dynamic>();
                _tokens.TryAdd(id, taskSource);
                Task.Run(async () => await SendMessageAsync(message));
                return taskSource.Task;
            }
            catch (Exception e)
            {
                throw new SendMessageException("Can't send message", e);
            }
        }


        public Task<dynamic> SendCommandAsync(RequestMessage message)
        {
            Interlocked.Increment(ref _commandCount);
            var rawMessage = new RawRequestMessage(message, _commandCount);
            var serialized = JsonConvert.SerializeObject(rawMessage, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            return SendCommandAsync(rawMessage.Id, serialized);
        }

        public async Task CloseAsync()
        {
            await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
        }

        public void Dispose()
        {
            _socket?.Dispose();
            _socket = null;
        }
    }
}
