using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos.Entradas;

[QueryProperty(nameof(CurrentEntrada), "entrada")]
public partial class PgEntAddEditVistaModelo : ObservableValidator
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    Entrada? currentEntrada;

    public string Titulo => currentEntrada is null ? "Nueva - Entrada" : "Modificar - Entrada";

    [ObservableProperty]
    DateTime fecha = DateTime.Now;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string producto;

    [ObservableProperty]
    [Required]
    [Range(1, 1000)]
    double cantidad;

    [ObservableProperty]
    [Required]
    [Range(1, 19999.99)]
    double precio;

    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    string proveedor;

    [ObservableProperty]
    double costoFlete;

    [ObservableProperty]
    double costoCarga;

    [ObservableProperty]
    bool visibleError;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentEntrada))
        {
            if (currentEntrada is not null)
            {
                Fecha = currentEntrada.Fecha;
                Producto = currentEntrada.Producto;
                Cantidad = currentEntrada.Cantidad;
                Precio = (double)currentEntrada.Precio;
                NoFactura = currentEntrada.NoFactura;
                Proveedor = currentEntrada.Proveedor;
                CostoFlete = (double)currentEntrada.CostoFlete;
                CostoCarga = (double)currentEntrada.CostoCarga;
            }
        }
    }

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

        var newEntrada = new Entrada(fecha,producto, (int)cantidad, (decimal)precio, noFactura, proveedor, (decimal)costoFlete, (decimal)costoCarga);
        WeakReferenceMessenger.Default.Send<Entrada>(newEntrada);
        await Cancelar();
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }
}
