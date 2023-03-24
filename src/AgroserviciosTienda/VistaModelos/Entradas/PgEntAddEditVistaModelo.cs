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
    EntradaView currentEntrada;

    public string Titulo => currentEntrada is null ? "Nueva - Entrada" : "Modificar - Entrada";

    [ObservableProperty]
    DateTime fecha = DateTime.Now;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string productoNombre;

    [ObservableProperty]
    [Required]
    [Range(1, 1000)]
    double cantidad = 0;

    [ObservableProperty]
    [Required]
    [Range(1, 19999.99)]
    double precio = 0;

    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    string proveedor;

    [ObservableProperty]
    double costoFlete = 0;

    [ObservableProperty]
    double costoCarga = 0;

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
                ProductoNombre = currentEntrada.Producto.Nombre;
                Cantidad = currentEntrada.Producto.Cantidad;
                Precio = (double)currentEntrada.Producto.Precio;
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

        var newProducto = new Producto(productoNombre, (int)cantidad, (decimal)precio);
        EntradaView newEntrada = string.IsNullOrEmpty(noFactura) 
            ? new EntradaView() { Fecha = fecha, Producto = newProducto } 
            : new EntradaView() { Fecha = fecha, Producto = newProducto, NoFactura = noFactura, Proveedor = proveedor, CostoFlete = (decimal)costoFlete, CostoCarga = (decimal)costoCarga };

        WeakReferenceMessenger.Default.Send<EntradaView>(newEntrada);
        await Cancelar();
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }
}
