using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos.Ventas;

[QueryProperty(nameof(EsNuevo), "t")]
public partial class PgVenAddEditVistaModelo : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    bool esNuevo;

    public string Titulo => esNuevo! ? "Nueva - Venta" : "Modificar - Venta";
}
