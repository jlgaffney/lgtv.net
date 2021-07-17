using System.Globalization;

namespace LgTv.Clients.Info
{
    public class SoftwareInformation
    {
		public string ProductName { get; set; }

		public string ModelName { get; set; }

		public string SoftwareType { get; set; }

		public string MajorVersion { get; set; }

		public string MinorVersion { get; set; }

		public string Country { get; set; }

		public string CountryGroup { get; set; }

		public string DeviceId { get; set; }

		public string AuthFlag { get; set; }

		public string IgnoreDisable { get; set; }

		public string EcoInfo { get; set; }

		public string ConfigKey { get; set; }

		public CultureInfo Language { get; set; }
	}
}
