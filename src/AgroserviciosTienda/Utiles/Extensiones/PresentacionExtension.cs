using AgroserviciosTienda.Modelos;
using UnitsNet;
using UnitsNet.Units;

namespace AgroserviciosTienda.Utiles.Extensiones;

public static class PresentacionExtension
{
    public static EntradaView ToEntradaView(this Entrada entrada)
    {
        return new EntradaView(
            entrada.Fecha,
            entrada.Productos,
            entrada.NoFactura,
            entrada.Proveedor,
            entrada.CostoFlete,
            entrada.CostoCarga
        );
    }
    
    //public static IQuantity ToIQuantity(this Presentacion presentacion)
    //{
    //    return Quantity.From(500, LengthUnit.Centimeter);
    //}
}
