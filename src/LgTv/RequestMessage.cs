using Newtonsoft.Json;

namespace LgTv
{
    public class RequestMessage
    {
        // TODO Improve constructor signatures

        public RequestMessage(string uri)
        {
            Uri = uri;
        }

        public RequestMessage(string prefix, string uri)
        {
            Prefix = prefix;
            Uri = uri;
        }

        public RequestMessage(string uri, object payload)
        {
            Uri = uri;
            SetPayload(payload);
        }

        public string Prefix { get; set; }

        public string Type { get; set; }

        public string Uri { get; set; }

        public string Payload { get; set; }


        public void SetPayload(object payload)
        {
            Payload = payload is string payloadString ? payloadString : JsonConvert.SerializeObject(payload);
        }
    }
}