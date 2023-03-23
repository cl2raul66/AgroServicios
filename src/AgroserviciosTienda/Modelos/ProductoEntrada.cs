namespace AgroserviciosTienda.Modelos;

public class ProductoEntrada
{
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    public ProductoEntrada(string nombre, int cantidad, decimal precio)
    {
        Nombre = nombre; 
        Cantidad = cantidad; 
        Precio = precio;
    }
}