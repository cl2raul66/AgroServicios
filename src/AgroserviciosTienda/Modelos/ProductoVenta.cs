namespace AgroserviciosTienda.Modelos;

public class ProductoVenta
{
    public Producto ElProducto { get; set; }
    public double Cantidad { get; set; }
    public double Precio { get; set; }

    public ProductoVenta() { }

    public ProductoVenta(Producto elproducto, double cantidad, double precio)
    {
        ElProducto = elproducto;
        Precio = precio;
        Cantidad = cantidad;
    }
}
