using System.Globalization;

namespace AgroserviciosTienda.Utiles.Convertidores;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool boolValue = (bool)value;
        string hexColor = boolValue ? "#DFD8F7" : "#512BD4";
        return Color.FromArgb(hexColor);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

