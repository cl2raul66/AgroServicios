using System.Globalization;

namespace AgroserviciosTienda.Utiles.Convertidores;

public class DoubleCeroToNullConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue && doubleValue == 0)
        {
            return null;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
