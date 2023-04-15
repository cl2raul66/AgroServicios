namespace AgroserviciosTienda.Modelos;

public record Producto
(
    string Nombre, 
    int Cantidad, 
    decimal Precio,
    Empaque Presentacion
);
