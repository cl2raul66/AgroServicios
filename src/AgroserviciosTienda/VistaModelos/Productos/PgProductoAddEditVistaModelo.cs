using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Servicios;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(DatosNav), "productodatosNav")]
public partial class PgProductoAddEditVistaModelo : ObservableValidator
{
    readonly IMedidasServicio medidasServ;

    public PgProductoAddEditVistaModelo(IMedidasServicio medidasServicio)
    {
        medidasServ = medidasServicio;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(DatosNav))
        {
            CurrentProducto = DatosNav.Item1;
        }

        if (e.PropertyName == nameof(CurrentProducto))
        {
            if (CurrentProducto is not null)
            { 
                ProductoNombre = CurrentProducto.ElProducto.Nombre;
                Cantidad = CurrentProducto.CantidadUnidad;
                Precio = CurrentProducto.Precio;
            }
        }
    }

    [ObservableProperty]
    Tuple<ProductoEntrada, bool> datosNav; //Producto currentProducto, bool esVender

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    ProductoEntrada currentProducto;

    public string Titulo => $"Producto - {(CurrentProducto is null ? "Agregar" : "Modificar")}";

    [ObservableProperty]
    public List<string> productosNombre;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string productoNombre;

    public IEnumerable<string> TiposMedidas => medidasServ.TiposMedidas;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Unidades))]
    string selectedTipoMedidas;

    public ObservableCollection<string> Unidades => new(medidasServ.Unidades(SelectedTipoMedidas));

    [ObservableProperty]
    string selectedUnidades;

    [ObservableProperty]
    [Required]
    [Range(0.01, 10000.00)]
    double cantidadPresentacion;

    [ObservableProperty]
    [Required]
    [Range(1, 1000)]
    int cantidad;

    [ObservableProperty]
    [Required]
    [Range(1, 19999.99)]
    double precio;

    [ObservableProperty]
    bool visibleError;

    [RelayCommand]
    private async void Agregar()
    {
        if (await Guardar())
        {
            ProductoNombre = string.Empty;
            CurrentProducto = null;
            SelectedTipoMedidas = null;
            SelectedUnidades = null;
            CantidadPresentacion = 0;
            Cantidad = 0;
            Precio = 0;
        }
    }

    [RelayCommand]
    private async void AgregarSalir()
    {
        if (await Guardar())
        {
            await Cancelar();
        }
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }

    #region extra
    async Task<bool> Guardar()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return false;
        }

        var p = new Producto() { Nombre = ProductoNombre, Presentacion = new Empaque(SelectedTipoMedidas, SelectedUnidades, CantidadPresentacion) };
        var newProducto = new ProductoEntrada() { ElProducto = p, CantidadUnidad = Cantidad, Precio = Precio };
        var resul = WeakReferenceMessenger.Default.Send<ProductoEntrada>(newProducto);
        return resul is not null;
    }
    #endregion
}
