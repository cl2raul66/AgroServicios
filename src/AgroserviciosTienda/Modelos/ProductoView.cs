using UnitsNet;

namespace AgroserviciosTienda.Modelos;

public class ProductoView
{
    public string Nombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public IQuantity Presentacion { get; set; }

    public ProductoView(string nombre, int cantidad, decimal precio, IQuantity presentacion)
    {
        Nombre = nombre; Cantidad = cantidad; Precio = precio;
        Presentacion = presentacion; 
    }
}