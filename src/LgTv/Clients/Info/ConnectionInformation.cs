namespace LgTv.Info
{
    public class ConnectionInformation
    {
        public bool Subscribed { get; set; }

        public ConnectionDeviceInfo P2PInfo { get; set; }
        
        public ConnectionDeviceInfo WifiInfo { get; set; }
        
        public ConnectionDeviceInfo WiredInfo { get; set; }
    }
}
