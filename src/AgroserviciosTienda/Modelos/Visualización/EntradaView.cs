namespace AgroserviciosTienda.Modelos;

public class EntradaView
{
    public string Fecha { get; set; }
    public string Productos { get; set; }
    public string NoFactura { get; set; }
    public string Vendedor { get; set; }
    public decimal CostoTotal { get; set; }

    public EntradaView(Entrada entrada)
    {
        Fecha = entrada.Fecha.ToString("dd/MM/yyyy");
        Productos = string.Join("\n", entrada.Productos.Select(x => $"-{x.Articulo}")).TrimEnd();
        NoFactura=entrada.NoFactura;
        Vendedor = entrada.Vendedor?.Nombre;
        CostoTotal = (decimal)entrada.Productos.Sum(x => x.Precio * x.CantidadUnidad) + entrada.CostoCarga + entrada.CostoFlete;
    }

}
