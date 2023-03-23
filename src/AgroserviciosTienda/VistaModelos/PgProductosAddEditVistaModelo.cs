using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(CurrentProducto), "producto")]
public partial class PgProductosAddEditVistaModelo : ObservableValidator
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    ProductoVenta currentProducto;

    public string Titulo => currentProducto is null ? "Nueva - producto" : "Modificar - producto";

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
    bool visibleError;

    [RelayCommand]
    private async void Agregar()
    {
        if (await Guardar())
        {
            ProductoNombre = string.Empty;
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

        var newProucto = new ProductoVenta(productoNombre, (int)cantidad, (decimal)precio);
        var resul = WeakReferenceMessenger.Default.Send<ProductoVenta>(newProucto);
        return resul is not null;
    }
    #endregion
}
