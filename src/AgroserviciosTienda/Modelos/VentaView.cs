namespace AgroserviciosTienda.Modelos;

public class VentaView
{
    public DateTime Fecha { set; get; }
    public List<Producto> Productos { set; get; }

    public VentaView() { }
    public VentaView(DateTime fecha, List<Producto> productos) {
        Fecha = fecha;
        Productos = productos;
    }
}
