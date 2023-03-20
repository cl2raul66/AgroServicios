namespace AgroserviciosTienda.Modelos;

public record Venta(DateTime Fecha, string Producto, int Cantidad, decimal Precio, string NoFactura, string Cliente);
