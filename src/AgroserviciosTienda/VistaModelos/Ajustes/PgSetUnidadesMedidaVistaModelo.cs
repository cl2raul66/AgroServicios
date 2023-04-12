using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using UnitsNet;
using UnitsNet.Units;

namespace AgroserviciosTienda.VistaModelos.Ajustes;

public partial class PgSetUnidadesMedidaVistaModelo : ObservableObject
{
    public PgSetUnidadesMedidaVistaModelo()
    {
        //Medidas = Quantity.Names.Select(n =>
        //{
        //    var u = Quantity.ByName[n];
        //    var unitSystem = UnitSystem.SI;
        //    return new MedidaView
        //    {
        //        Nombre = n,
        //        Unidades = u.UnitInfos.Select(x => x.Name).ToList()
        //    };
        //});
        var n = Quantity.Names;
        var i = Quantity.Infos;
    }

    [ObservableProperty]
    IEnumerable<MedidaView> medidas;

    //IEnumerable<string> GetUnitsForQuantityType(string name)
    //{
    //    var qi = Quantity.ByName[name];
    //    var t = qi.UnitInfos.Select(x => x.BaseUnits;


    //    var u = Length.Units;
    //    return u.Select(x => Length.GetAbbreviation(x));
    //}
}
