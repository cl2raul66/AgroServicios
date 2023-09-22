using System.Globalization;

namespace AgroserviciosTienda.Utiles.Convertidores;

internal class TipoAlertaStringToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)value switch
        {
            nameof(TipoAlerta.Adventencia) => Colors.Orange,
            nameof(TipoAlerta.Error) => Colors.Red,
            nameof(TipoAlerta.Informacio) => Colors.LightYellow,
            _ => Colors.LightYellow
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
