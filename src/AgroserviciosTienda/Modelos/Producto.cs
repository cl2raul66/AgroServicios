namespace AgroserviciosTienda.Modelos;

//public record Producto
//(
//    string Nombre, 
//    int Cantidad, 
//    decimal Precio,
//    Empaque Presentacion
//);

public class Producto
{
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public Empaque Presentacion { get; set; }

    public Producto(string nombre, int cantidad, decimal precio, Empaque presentacion)
    {
        Nombre = nombre;
        Cantidad = cantidad;
        Precio = precio;
        Presentacion = presentacion;
    }
}
