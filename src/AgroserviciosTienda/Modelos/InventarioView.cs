namespace AgroserviciosTienda.Modelos;

public class InventarioView
{
    public string Articulo { get; set; }
    public double Existencia { get; set; }
    public decimal PrecioInicial { get; set; }

    public InventarioView(Inventario inventario)
    {
        Articulo = inventario.Articulo.ToString();
        Existencia = inventario.Existencia;
        PrecioInicial = Preferences.Get(inventario.Id, 0);
    }
}
