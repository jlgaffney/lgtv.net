namespace LgTv.Scanning
{
    public class Device
    {
        public Device(
            string id,
            string name,
            string ipAddress)
        {
            Id = id;
            Name = name;
            IpAddress = ipAddress;
        }

        public string Id { get; }

        public string Name { get; }

        public string IpAddress { get; }
    }
}
