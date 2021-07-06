using Newtonsoft.Json;

namespace LgTv
{
    public class RequestMessage
    {
        public RequestMessage(string prefix, string uri)
        {
            Prefix = prefix;
            Uri = uri;
        }

        public RequestMessage(string uri, object payload)
        {
            Uri = uri;
            Payload = JsonConvert.SerializeObject(payload);
        }

        public string Prefix { get; set; }

        public string Type { get; set; }

        public string Uri { get; set; }

        public string Payload { get; set; }
    }
}