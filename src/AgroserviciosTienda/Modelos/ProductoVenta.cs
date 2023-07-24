namespace AgroserviciosTienda.Modelos;

public class ProductoVenta
{
    public Producto Articulo { get; set; }
    public int CantidadUnidad { get; set; }
    public double Precio { get; set; }

    public ProductoVenta() { }

    public ProductoVenta(Producto articulo, int cantidadunidad, double precio)
    {
        Articulo = articulo;
        CantidadUnidad = cantidadunidad;
        Precio = precio;
    }

    public override string ToString()
    {
        return Articulo.ToString();
    }
}
