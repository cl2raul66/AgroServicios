using AgroserviciosTienda.Modelos;
using System.Globalization;

namespace AgroserviciosTienda.Utiles.Convertidores;

public class ProductosVentaTotalConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not List<ProductoVenta> productos)
        {
            return null;
        }

        decimal total = 0;

        foreach (ProductoVenta producto in productos)
        {
            total += producto.Cantidad * producto.Precio;
        }

        return total.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
