using AgroserviciosTienda.Servicios;
using AgroserviciosTienda.Vistas;
using Android.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Converters;
using GoogleGson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgMedidasDetallesVistaModelo : ObservableObject
{
    readonly IMedidasServicio medidasServ;

    public PgMedidasDetallesVistaModelo(IMedidasServicio medidasServicio)
    {
        medidasServ = medidasServicio;

        magnitudesTodas = medidasServ.MagnitudAll;

        GetVistamagnitudesuso();
    }

    #region Magnitudes
    [ObservableProperty]
    IEnumerable<string> magnitudesTodas;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoAgregarQuitarMagnitud))]
    string selectedMagnitud;

    [ObservableProperty]
    string vistaMagnitudesuso;

    public string TextoAgregarQuitarMagnitud => medidasServ.MagnitudesUso.Contains(SelectedMagnitud)
        ? "Eliminar magnitud"
        : "Agregar magnitud";

    [RelayCommand]
    async Task AgregarQuitarMagnitud()
    {
        if (TextoAgregarQuitarMagnitud[0] == 'A')
        {
            medidasServ.InsertarMagnitudUso(SelectedMagnitud);
        }
        else
        {
            medidasServ.EliminarMagnitudUso(SelectedMagnitud);
        }

        SelectedMagnitud = null;
        GetVistamagnitudesuso();

        await Task.CompletedTask;
    }
    #endregion

    [RelayCommand]
    async Task GoToBack()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    #region Extra
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
    }

    void GetVistamagnitudesuso()
    {
        VistaMagnitudesuso = medidasServ.MagnitudesUso.Count() == MagnitudesTodas.Count()
            ? "Todas"
            : string.Join(", ", medidasServ.MagnitudesUso).TrimEnd();
    }
    #endregion
}
