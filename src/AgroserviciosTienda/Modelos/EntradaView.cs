namespace AgroserviciosTienda.Modelos;

public class EntradaView
{
    public DateTime Fecha { get; set; }
    public List<Producto> Productos { get; set; }
    public string NoFactura { get; set; }
    public Contacto Proveedor { get; set; }
    public decimal CostoFlete { get; set; }
    public decimal CostoCarga { get; set; }

    public EntradaView() { }
    public EntradaView(DateTime fecha, List<Producto> productos)
    {
        Fecha = fecha;
        Productos = productos;
    }
    public EntradaView(DateTime fecha, List<Producto> productos, string nofactura, Contacto proveedor, decimal costoflete, decimal costocarga)
    {
        Fecha = fecha;
        Productos = productos;
        NoFactura = nofactura;
        Proveedor = proveedor;
        CostoFlete = costoflete;
        CostoCarga = costocarga;
    }
}
