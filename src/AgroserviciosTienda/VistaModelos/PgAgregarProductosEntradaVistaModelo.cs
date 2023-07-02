using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Servicios;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgAgregarProductosEntradaVistaModelo : ObservableValidator
{
    readonly IMedidasServicio medidasServ;

    public PgAgregarProductosEntradaVistaModelo(IMedidasServicio medidasServicio)
    {
        medidasServ = medidasServicio;
        Medidas = medidasServ.MagnitudAll;
    }

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string nombre;

    [ObservableProperty]
    IEnumerable<string> medidas;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Unidades))]
    string presentacionMedida;

    public ObservableCollection<string> Unidades => new(medidasServ.Unidades(PresentacionMedida));

    [ObservableProperty]
    string presentacionUnidad;

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
            PresentacionMedida = null;
            PresentacionUnidad = null;
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

        var newProductoEntrada = new ProductoEntrada(new() { Nombre = Nombre.TrimEnd(), Presentacion = new Empaque(PresentacionMedida, PresentacionUnidad, PresentacionValor) }, Cantidadunidad, Precio);
        var resul = WeakReferenceMessenger.Default.Send(newProductoEntrada);
        return resul is not null;
    }
    #endregion
}
