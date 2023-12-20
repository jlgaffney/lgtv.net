using System.Globalization;

namespace LgTv.Sample.Maui.Converters;

public class ServiceLocatorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not Type serviceType)
        {
            return null;
        }

        return ServiceLocator.Get(serviceType);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
