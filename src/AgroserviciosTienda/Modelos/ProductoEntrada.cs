namespace AgroserviciosTienda.Modelos;

public class ProductoEntrada
{
    public Producto ElProducto { get; set; }
    public int CantidadUnidad { get; set; }
    public double Precio { get; set; }

    public ProductoEntrada() { }

    public ProductoEntrada(Producto elproducto, int cantidadunidad, double precio)
    {
        ElProducto = elproducto;
        CantidadUnidad = cantidadunidad;
        Precio = precio;
    }
}