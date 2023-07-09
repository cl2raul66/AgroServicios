using AgroserviciosTienda.Servicios;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AgroserviciosTienda.Modelos;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgMedidasDetallesVistaModelo : ObservableObject
{
    readonly IBaseMedidasServicio baseMedidasServ;
    readonly IMedidasServicio medidasServ;

    public PgMedidasDetallesVistaModelo(IBaseMedidasServicio baseMedidasServicio, IMedidasServicio medidasServicio)
    {
        baseMedidasServ = baseMedidasServicio;
        medidasServ = medidasServicio;

        magnitudesTodas = baseMedidasServ.AllNombresMagnitud;
        magnitudesUso = new(medidasServ.AllMagnitud);
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

    public string TextoAgregarQuitarMagnitud => medidasServ.ExisteMagnitud(SelectedMagnitud)
        ? "No usar magnitud"
        : "Usar magnitud";

    [RelayCommand]
    async Task AgregarQuitarMagnitud()
    {
        if (TextoAgregarQuitarMagnitud[0] == 'U')
        {
            medidasServ.AgregarMagnitud(SelectedMagnitud);
        }
        else
        {
            medidasServ.QuitarMagnitud(SelectedMagnitud);
        }

        MagnitudesUso = new(medidasServ.AllMagnitud);
        SelectedMagnitud = null;
        GetVistamagnitudesuso();

        await Task.CompletedTask;
    }
    #endregion

    #region Unidades
    [ObservableProperty]
    ObservableCollection<string> magnitudesUso;

    [ObservableProperty]
    string selectedMagnitudesuso;

    [ObservableProperty]
    ObservableCollection<TipoUnidad> unidades;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TextoAgregarQuitarUnidad))]
    TipoUnidad selectedUnidad;

    [ObservableProperty]
    ObservableCollection<UnidadUsoWiew> vistaUnidadesuso;

    public string TextoAgregarQuitarUnidad => medidasServ.ExisteUnidad(
        string.IsNullOrEmpty(SelectedMagnitudesuso)
        ? medidasServ.AllMagnitud.First()
        : SelectedMagnitudesuso, SelectedUnidad)
        ? "No usar unidad"
        : "Usar unidad";

    [RelayCommand]
    async Task AgregarQuitarUnidad()
    {
        if (TextoAgregarQuitarUnidad[0] == 'U')
        {
            medidasServ.AgregarUnidad(SelectedMagnitudesuso, SelectedUnidad);
        }
        else
        {
            medidasServ.QuitarUnidad(SelectedMagnitudesuso, SelectedUnidad);
        }

        SelectedUnidad = null;
        GetVistaunidadesuso();
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

        if (e.PropertyName == nameof(SelectedMagnitudesuso))
        {
            Unidades = new(baseMedidasServ.AllUnidad(SelectedMagnitudesuso));
            GetVistaunidadesuso();
        }
        
        if (e.PropertyName == nameof(SelectedUnidad))
        {
            
        }
    }

    void GetVistamagnitudesuso()
    {
        VistaMagnitudesuso = medidasServ.AllMagnitud.Count() == baseMedidasServ.AllMagnitud.Count()
            ? "Todas"
            : string.Join(", ", medidasServ.AllMagnitud).TrimEnd();
    }

    void GetVistaunidadesuso()
    {
        VistaUnidadesuso = new(medidasServ.AllMagnitud
            .Where(baseMedidasServ.ExisteMagnitud)
            .Select(v =>
            {
                var baseUnidades = baseMedidasServ.AllUnidad(v);
                var usoUnidades = medidasServ.AllUnidades(v);
                var equalUnidades = baseUnidades.All(u1 => usoUnidades.Any(u2 => u2.Equals(u1)));
                string unidadesString = equalUnidades ? "Todos" : string.Join(", ", usoUnidades.Select(x => x.Nombre));
                return new UnidadUsoWiew(v, unidadesString);
            }));
    }
    #endregion
}
