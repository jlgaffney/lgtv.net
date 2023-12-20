using System.Globalization;

namespace LgTv.Sample.Maui.Converters;

public class CustomUriImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string uri)
        {
            return null;
        }

        return new CustomUriImageSource
        {
            Uri = new Uri(uri)
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
