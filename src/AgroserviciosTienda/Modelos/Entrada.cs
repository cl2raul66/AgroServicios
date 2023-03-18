namespace AgroserviciosTienda.Modelos;

public record Entrada(DateTime Fecha, string Producto, int Cantidad, decimal Precio, string NoFactura, string Proveedor, decimal CostoFlete, decimal CostoCarga);

