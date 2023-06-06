using AgroserviciosTienda.Servicios;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgAjustesVistaModelo : ObservableObject
{
    readonly IMedidasServicio medidasServ;

    public PgAjustesVistaModelo(IMedidasServicio medidasServicio)
    {
        medidasServ = medidasServicio;
        Medidas = medidasServ.TiposMedidas;
    }

    [ObservableProperty]
    IEnumerable<string> medidas;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Unidades))]
    string selectedMedida;

    public ObservableCollection<string> Unidades => new(medidasServ.Unidades(SelectedMedida));
}
