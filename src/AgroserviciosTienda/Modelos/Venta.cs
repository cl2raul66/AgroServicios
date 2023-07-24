namespace AgroserviciosTienda.Modelos;

public class Venta
{
    public DateTime Fecha { get; set; }
    public List<ProductoVenta> Productos { get; set; }
    public string NoFactura { get; set; }
    public Cliente Comprador { get; set; } = null;

    public Venta(DateTime fecha, List<ProductoVenta> productos)
    {
        Fecha = fecha;
        Productos = productos;
        NoFactura = string.Empty;
    }

    public Venta(DateTime fecha, List<ProductoVenta> productos, string noFactura, Cliente comprador)
    {
        Fecha = fecha;
        Productos = productos;
        NoFactura = noFactura;
        Comprador = comprador;
    }

    public Venta() { }
}
