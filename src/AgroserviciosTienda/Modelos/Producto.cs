namespace AgroserviciosTienda.Modelos;

public class Producto
{
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    public Producto(string nombre, int cantidad, decimal precio)
    {
        Nombre = nombre; Cantidad = cantidad; Precio = precio;
    }
}