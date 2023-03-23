namespace AgroserviciosTienda.Modelos;

public class ProductoVenta
{
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    public ProductoVenta(string nombre, int cantidad, decimal precio)
    {
        Nombre = nombre; Cantidad = cantidad; Precio = precio;
    }
}