using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos.Ventas;

[QueryProperty(nameof(CurrentVenta), "venta")]
public partial class PgVenAddEditVistaModelo : ObservableValidator
{
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentVenta))
        {
            if (currentVenta is not null)
            {
                Fecha = currentVenta.Fecha;
                Productos = new(currentVenta.Productos);
            }
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    VentaView currentVenta;

    public string Titulo => currentVenta is null ? "Nueva - Venta" : "Modificar - Venta";

    [ObservableProperty]
    DateTime fecha = DateTime.Now;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string productoNombre;

    [ObservableProperty]
    [Required]
    [Range(1, 1000)]
    double cantidad;

    [ObservableProperty]
    [Required]
    [Range(1, 19999.99)]
    double precio;

    [ObservableProperty]
    ObservableCollection<Producto> productos = new();

    [ObservableProperty]
    Producto selectedProducto;

    [ObservableProperty]
    bool visibleError;

    [RelayCommand]
    private async void Guardar()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return;
        }

        Productos.Add(new(productoNombre, (int)cantidad, (decimal)precio));
        ProductoNombre = string.Empty;
        Cantidad = 0;
        Precio = 0;
    }

    [RelayCommand]
    private async void GuardarSalir()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return;
        }

        var newVenta = new VentaView(fecha, productos.ToList());
        WeakReferenceMessenger.Default.Send<VentaView>(newVenta);
        await Cancelar();
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }
}
