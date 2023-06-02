namespace AgroserviciosTienda.Modelos;

public class ProductoEntrada
{
    public Producto Articulos { get; set; }
    public int CantidadUnidad { get; set; }
    public double Precio { get; set; }

    public ProductoEntrada() { }

    public ProductoEntrada(Producto articulos, int cantidadunidad, double precio)
    {
        Articulos = articulos;
        CantidadUnidad = cantidadunidad;
        Precio = precio;
    }
}
