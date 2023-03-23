namespace AgroserviciosTienda.Modelos;

public class EntradaView
{
    public DateTime Fecha { get; set; }
    public ProductoEntrada Producto { get; set; }
    public string NoFactura { get; set; }
    public string Proveedor { get; set; }
    public decimal CostoFlete { get; set; }
    public decimal CostoCarga { get; set; }

    public EntradaView() { }
    public EntradaView(DateTime fecha, ProductoEntrada producto)
    {
        Fecha = fecha;
        Producto = producto;
    }
    public EntradaView(DateTime fecha, ProductoEntrada producto, string nofactura, string proveedor, decimal costoflete, decimal costocarga)
    {
        Fecha = fecha;
        Producto = producto;
        NoFactura = nofactura;
        Proveedor = proveedor;
        CostoFlete = costoflete;
        CostoCarga = costocarga;
    }
}
