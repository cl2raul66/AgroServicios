using AgroserviciosTienda.Modelos;

namespace AgroserviciosTienda.Utiles.Extension;

public static class EntradaToEntradaView
{
    public static EntradaView ToEntradaView(this Entrada entrada)
    {
        var detalle = $"Producto: {entrada.Producto}, Cantidad: {entrada.Cantidad}, Precio: {entrada.Precio:C}";

        if (!string.IsNullOrEmpty(entrada.NoFactura))
        {
            detalle += $", Proveedor: {entrada.Proveedor}";
        }
        
        if (!string.IsNullOrEmpty(entrada.Proveedor))
        {
            detalle += $", Proveedor: {entrada.Proveedor}";
        }

        return new EntradaView
        {
            Texto = entrada.Fecha.ToShortDateString(),
            Detalle = detalle
        };
    }
}
