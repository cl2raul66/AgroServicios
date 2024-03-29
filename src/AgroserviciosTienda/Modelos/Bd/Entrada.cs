﻿namespace AgroserviciosTienda.Modelos;

public class Entrada
{
    public DateTime Fecha { get; set; }
    public List<ProductoEntrada> Productos { get; set; }
    public string NoFactura { get; set; }
    public Proveedor Vendedor { get; set; } = null;
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

    public Entrada(DateTime fecha, List<ProductoEntrada> productos, string noFactura, Proveedor vendedor, decimal costoFlete, decimal costoCarga)
    {
        Fecha = fecha;
        Productos = productos;
        NoFactura = noFactura;
        Vendedor = vendedor;
        CostoFlete = costoFlete;
        CostoCarga = costoCarga;
    }

    public Entrada()
    {

    }
}
