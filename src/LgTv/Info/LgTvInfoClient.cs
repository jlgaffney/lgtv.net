using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace LgTv.Info
{
    public class LgTvInfoClient : ILgTvInfoClient
    {
        private readonly ILgTvConnection _connection;

        public LgTvInfoClient(
            ILgTvConnection connection)
        {
            _connection = connection;
        }

        public async Task<SystemInformation> GetSystemInfo()
        {
            var requestMessage = new RequestMessage("ssap://system/getSystemInfo");
            var response = await _connection.SendCommandAsync(requestMessage);

            SystemFeatures features = null;
            if (response is JObject jsonObject)
            {
                var featuresObject = jsonObject["features"];

                features = new SystemFeatures
                {
                    _3D = (bool) featuresObject["3d"],
                    DVR = (bool) featuresObject["dvr"]
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
            var requestMessage = new RequestMessage("ssap://com.webos.service.update/getCurrentSWInformation");
            var response = await _connection.SendCommandAsync(requestMessage);

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
            var requestMessage = new RequestMessage("ssap://com.webos.service.connectionmanager/getinfo");
            var response = await _connection.SendCommandAsync(requestMessage);

            var info = new ConnectionInformation
            {
                Subscribed = response.subscribed
            };

            if (response.P2PInfo != null)
            {
                info.P2PInfo = new ConnectionDeviceInfo
                {
                    MacAddress = response.P2PInfo.macAddress
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
            var requestMessage = new RequestMessage("ssap://api/getServiceList");
            var response = await _connection.SendCommandAsync(requestMessage);

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
}
