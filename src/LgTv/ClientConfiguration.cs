namespace LgTv;

public class ClientConfiguration
{
    public string ClientKeyStoreFilePath { get; set; }

    public HostConfiguration Tv { get; set; }

    public HostConfiguration Proxy { get; set; }
}
