using System.Globalization;
using LgTv.Connections;
using Newtonsoft.Json.Linq;

namespace LgTv.Clients.Info;

public class LgTvInfoClient(ILgTvConnection connection) : ILgTvInfoClient
{
    public async Task<DateTime> GetCurrentTime()
    {
        var requestMessage = new RequestMessage(LgTvCommands.GetCurrentTime.Uri);
        var response = await connection.SendCommandAsync(requestMessage);

        var time = response.time;

        if (time == null)
        {
            return DateTime.MinValue;
        }

        return new DateTime(
            int.Parse((string) time.year),
            int.Parse((string) time.month),
            int.Parse((string) time.day),
            int.Parse((string) time.hour),
            int.Parse((string) time.minute),
            int.Parse((string) time.second));
    }

    public async Task<SystemInformation> GetSystemInfo()
    {
        var requestMessage = new RequestMessage(LgTvCommands.GetSystemInfo.Uri);
        var response = await connection.SendCommandAsync(requestMessage);

        SystemFeatures features = null;
        if (response is JObject jsonObject)
        {
            var featuresObject = jsonObject["features"];

            var _3d = featuresObject["3d"];
            var dvr = featuresObject["dvr"];

            features = new SystemFeatures
            {
                _3D = _3d != null && (bool) _3d,
                DVR = dvr != null && (bool) dvr
            };
        }

        var systemInfo = new SystemInformation
        {
            Features = features,
            ReceiverType = response.receiverType,
            ModelName = response.modelName,
            ProgramMode = Convert.ToBoolean(response.programMode)
        };

        return systemInfo;
    }

    public async Task<SoftwareInformation> GetSoftwareInfo()
    {
        var requestMessage = new RequestMessage(LgTvCommands.GetSoftwareInfo.Uri);
        var response = await connection.SendCommandAsync(requestMessage);

        CultureInfo language = null;
        if (response.language_code != null)
        {
            language = new CultureInfo((string) response.language_code);
        }

        var softwareInfo = new SoftwareInformation
        {
            ProductName = response.product_name,
            ModelName = response.model_name,
            SoftwareType = response.sw_type,
            MajorVersion = response.major_ver,
            MinorVersion = response.minor_ver,
            Country = response.country,
            CountryGroup = response.country_group,
            DeviceId = response.device_id,
            AuthFlag = response.auth_flag,
            IgnoreDisable = response.ignore_disable,
            EcoInfo = response.eco_info,
            ConfigKey = response.config_key,
            Language = language
        };

        return softwareInfo;
    }

    public async Task<ConnectionInformation> GetConnectionInfo()
    {
        var requestMessage = new RequestMessage(LgTvCommands.GetConnectionInfo.Uri);
        var response = await connection.SendCommandAsync(requestMessage);

        var info = new ConnectionInformation
        {
            Subscribed = response.subscribed
        };

        if (response.p2pInfo != null)
        {
            info.P2PInfo = new ConnectionDeviceInfo
            {
                MacAddress = response.p2pInfo.macAddress
            };
        }

        if (response.wifiInfo != null)
        {
            info.WifiInfo = new ConnectionDeviceInfo
            {
                MacAddress = response.wifiInfo.macAddress
            };
        }

        if (response.wiredInfo != null)
        {
            info.WiredInfo = new ConnectionDeviceInfo
            {
                MacAddress = response.wiredInfo.macAddress
            };
        }

        return info;
    }

    public async Task<IEnumerable<Service>> GetServices()
    {
        var requestMessage = new RequestMessage(LgTvCommands.GetServices.Uri);
        var response = await connection.SendCommandAsync(requestMessage);

        var services = new List<Service>();
        foreach (var service in response.services)
        {
            services.Add(new Service
            {
                Name = service.name,
                Version = int.Parse((string) service.version)
            });
        }

        return services;
    }
}