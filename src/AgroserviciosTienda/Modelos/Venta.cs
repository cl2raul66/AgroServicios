namespace AgroserviciosTienda.Modelos;

public class Venta
{
    public DateTime Fecha { set; get; }
    public List<ProductoVenta> Productos { set; get; }
    public string NoFactura { get; set; }
    public Contacto Cliente { get; set; }

    public Venta() { }
    public Venta(DateTime fecha, List<ProductoVenta> productos) {
        Fecha = fecha;
        Productos = productos;
    }
    public Venta(DateTime fecha, List<ProductoVenta> productos, string nofactura, Contacto cliente) {
        Fecha = fecha;
        Productos = productos;
        NoFactura = nofactura;
        Cliente = cliente;
    }
}
