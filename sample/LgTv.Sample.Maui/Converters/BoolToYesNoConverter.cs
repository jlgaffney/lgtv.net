using System.Globalization;

namespace LgTv.Sample.Maui.Converters;

public class BoolToYesNoConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool boolValue)
        {
            return null;
        }

        return boolValue ? "Yes" : "No";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
