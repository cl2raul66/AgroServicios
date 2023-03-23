namespace AgroserviciosTienda.Modelos;

public class VentaView
{
    public DateTime Fecha { set; get; }
    public List<ProductoVenta> Productos { set; get; }

    public VentaView() { }
    public VentaView(DateTime fecha, List<ProductoVenta> productos) {
        Fecha = fecha;
        Productos = productos;
    }
}
