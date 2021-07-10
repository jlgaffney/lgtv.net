using System;
using System.Reflection;

namespace LgTv.Extensions
{
    public static class OperatingSystemExtensions
    {
        private const string IsBrowserMethodName = "IsBrowser";

        private static readonly Type osType = typeof(OperatingSystem);

        public static bool IsBrowserPlatform(this OperatingSystem os)
        {
            var isBrowserMethodInfo = osType.GetMethod(IsBrowserMethodName, BindingFlags.Public | BindingFlags.Static);

            if (isBrowserMethodInfo == null)
            {
                return false;
            }

            var value = isBrowserMethodInfo.Invoke(null, Array.Empty<object>());

            if (!(value is bool result))
            {
                return false;
            }

            return result;
        }
    }
}
