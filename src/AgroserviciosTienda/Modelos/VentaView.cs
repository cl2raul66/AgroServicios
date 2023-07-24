namespace AgroserviciosTienda.Modelos;

public class VentaView
{
    public string Fecha { get; set; }
    public string Productos { get; set; }
    public string NoFactura { get; set; }
    public string Comprador { get; set; }
    public decimal TotalVenta { get; set; }

    public VentaView(Venta venta)
    {
        Fecha = venta.Fecha.ToString("dd/MM/yyyy");
        Productos = string.Join("\n", venta.Productos.Select(p => $"-{p.Articulo}")).TrimEnd();
        NoFactura = venta.NoFactura;
        Comprador = venta.Comprador.ToString();
        TotalVenta = (decimal)venta.Productos.Sum(p => p.Precio * p.CantidadUnidad);
    }
}
