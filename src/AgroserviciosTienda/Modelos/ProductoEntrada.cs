namespace AgroserviciosTienda.Modelos;

public class ProductoEntrada
{
    public Producto Articulo { get; set; }
    public int CantidadUnidad { get; set; }
    public double Precio { get; set; }

    public ProductoEntrada() { }

    public ProductoEntrada(Producto articulo, int cantidadunidad, double precio)
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


