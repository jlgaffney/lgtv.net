using System.Reflection;

namespace LgTv.Extensions;

public static class OperatingSystemExtensions
{
    private const string IsBrowserMethodName = "IsBrowser";

    private static readonly Type OsType = typeof(OperatingSystem);

    public static bool IsBrowserPlatform(this OperatingSystem os)
    {
        var isBrowserMethodInfo = OsType.GetMethod(IsBrowserMethodName, BindingFlags.Public | BindingFlags.Static);

        if (isBrowserMethodInfo == null)
        {
            return false;
        }

        var value = isBrowserMethodInfo.Invoke(null, []);

        if (value is not bool result)
        {
            return false;
        }

        return result;
    }
}
