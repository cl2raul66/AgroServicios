namespace AgroserviciosTienda.Modelos;

public record Entrada
(
    DateTime Fecha, 
    List<Producto> Productos, 
    string NoFactura, 
    Contacto Proveedor, 
    decimal CostoFlete, 
    decimal CostoCarga 
);