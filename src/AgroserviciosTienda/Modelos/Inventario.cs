namespace AgroserviciosTienda.Modelos;

public class Inventario
{
    public string Id { get; set; }
    public Producto Articulo { get; set; }
    public double Existencia { get; set; }
    public decimal PrecioInicial { get; set; }

    public Inventario() { PrecioInicial = 0; }

    public Inventario(Producto articulo, double existencia, string id = null)
    {
        Articulo = articulo;
        Existencia = existencia;
        Id = id;
        PrecioInicial = 0;
    }
}
