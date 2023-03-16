using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos.Entradas;

[QueryProperty(nameof(EsNuevo), "t")]
public partial class PgEntAddEditVistaModelo : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    bool esNuevo;

    public string Titulo => esNuevo! ? "Nueva - Entrada" : "Modificar - Entrada";
}
