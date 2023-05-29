namespace AgroserviciosTienda.Modelos;

//public record Entrada
//(
//    DateTime Fecha,
//    List<Producto> Productos,
//    string NoFactura,
//    Contacto Proveedor,
//    decimal CostoFlete,
//    decimal CostoCarga
//);

public class Entrada
{
    public DateTime Fecha { get; set; }
    public List<ProductoEntrada> Productos { get; set; }
    public string NoFactura { get; set; }
    public Contacto Proveedor { get; set; }
    public decimal CostoFlete { get; set; }
    public decimal CostoCarga { get; set; }

    public Entrada(DateTime fecha, List<ProductoEntrada> productos)
    {
        Fecha = fecha;
        Productos = productos;
        NoFactura = string.Empty;
        CostoFlete = 0;
        CostoCarga = 0;
    }

    public Entrada(DateTime fecha, List<ProductoEntrada> productos, string noFactura, Contacto proveedor, decimal costoFlete, decimal costoCarga)
    {
        Fecha = fecha;
        Productos = productos;
        NoFactura = noFactura;
        Proveedor = proveedor;
        CostoFlete = costoFlete;
        CostoCarga = costoCarga;
    }

    public Entrada()
    {

    }
}
