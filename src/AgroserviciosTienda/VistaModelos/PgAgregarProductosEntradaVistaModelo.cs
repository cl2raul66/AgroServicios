using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Servicios;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(Precio), nameof(Precio))]
[QueryProperty(nameof(Cantidadunidad), nameof(Cantidadunidad))]
public partial class PgAgregarProductosEntradaVistaModelo : ObservableValidator
{
    readonly IMedidasServicio medidasServ;

    public PgAgregarProductosEntradaVistaModelo(IMedidasServicio medidasServicio)
    {
        medidasServ = medidasServicio;
        Magnitudes = new(medidasServ.AllMagnitud);
    }

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string nombre;

    [ObservableProperty]
    ObservableCollection<string> magnitudes;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Unidades))]
    string selectedMagnitud;

    public ObservableCollection<TipoUnidad> Unidades => string.IsNullOrEmpty(SelectedMagnitud) ? new() : new(medidasServ.AllUnidades(SelectedMagnitud));

    [ObservableProperty]
    TipoUnidad selectedUnidad;

    [ObservableProperty]
    double presentacionValor;

    [ObservableProperty]
    [Required]
    [Range(1, 1000)]
    int cantidadunidad;

    [ObservableProperty]
    [Required]
    [Range(1, 19999.99)]
    double precio;

    [ObservableProperty]
    int countAdd;

    [ObservableProperty]
    bool visibleError = false;

    [RelayCommand]
    async Task Agregar()
    {
        if (await Guardar())
        {
            CountAdd++;
            Nombre = string.Empty;
            SelectedMagnitud = null;
            SelectedUnidad = null;
            PresentacionValor = 0;
            Cantidadunidad = 0;
            Precio = 0;
        }
    }

    [RelayCommand]
    async Task AgregarSalir()
    {
        if (await Guardar())
        {
            await Cancelar();
        }
    }

    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    #region extra
    async Task<bool> Guardar()
    {
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return false;
        }

        var newProductoEntrada = new ProductoEntrada(new() { Nombre = Nombre.TrimEnd(), Presentacion = new Empaque(SelectedMagnitud, SelectedUnidad.Nombre, PresentacionValor) }, Cantidadunidad, Precio);
        var resul = WeakReferenceMessenger.Default.Send(newProductoEntrada);
        return resul is not null;
    }
    #endregion
}
