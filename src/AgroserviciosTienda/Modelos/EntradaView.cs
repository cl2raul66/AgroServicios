namespace AgroserviciosTienda.Modelos;

public class EntradaView
{
    public string Texto { get; set; }
    public string Detalle { get; set; }

    public EntradaView() { }

    public EntradaView(Entrada entrada)
    {
        Texto = entrada.Fecha.ToShortDateString();
        Detalle = $"Producto: {entrada.Producto} - Cantidad: {entrada.Cantidad} - Precio: {entrada.Precio:C} - Proveedor: {entrada.Proveedor}";
    }
}
