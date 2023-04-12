using UnitsNet;

namespace AgroserviciosTienda.Utiles.Convertidores;

public class PresentacionToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is IQuantity quantity)
        {
            return $"{quantity.QuantityInfo.Name} - {quantity.Unit} - {quantity.Value}";
        }

        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
