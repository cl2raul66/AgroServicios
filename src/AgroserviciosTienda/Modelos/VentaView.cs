namespace AgroserviciosTienda.Modelos;

public class VentaView
{
    public DateTime Fecha { set; get; }
    public List<Producto> Productos { set; get; }
    public string NoFactura { get; set; }
    public Cliente Cliente { get; set; }

    public VentaView() { }
    public VentaView(DateTime fecha, List<Producto> productos) {
        Fecha = fecha;
        Productos = productos;
    }
    public VentaView(DateTime fecha, List<Producto> productos, string nofactura, Cliente cliente) {
        Fecha = fecha;
        Productos = productos;
        NoFactura = nofactura;
        Cliente = cliente;
    }
}
