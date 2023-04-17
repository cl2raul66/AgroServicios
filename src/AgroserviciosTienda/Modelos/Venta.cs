namespace AgroserviciosTienda.Modelos;

public class Venta
{
    public DateTime Fecha { set; get; }
    public List<Producto> Productos { set; get; }
    public string NoFactura { get; set; }
    public Contacto Cliente { get; set; }

    public Venta() { }
    public Venta(DateTime fecha, List<Producto> productos) {
        Fecha = fecha;
        Productos = productos;
    }
    public Venta(DateTime fecha, List<Producto> productos, string nofactura, Contacto cliente) {
        Fecha = fecha;
        Productos = productos;
        NoFactura = nofactura;
        Cliente = cliente;
    }
}
