using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(CurrentProveedor), "proveedor")]
[QueryProperty(nameof(VisibleAgregar), "visibleagregar")]
public partial class PgProveedorAddEditVistaModelo : ObservableValidator
{
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentProveedor))
        {
            if (currentProveedor is not null)
            {
                Nombre = currentProveedor.Nombre;
                Nit = currentProveedor.Nit;
                Telefono = currentProveedor.Telefono;
                Email = currentProveedor.EMail;
                Direccion = currentProveedor.Direccion;
            }
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    Proveedor currentProveedor;

    public string Titulo => currentProveedor is null ? "Nueva - Proveedor" : "Modificar - Proveedor";

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string nombre;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string nit;

    [ObservableProperty]
    [Phone]
    string telefono;

    [ObservableProperty]
    [EmailAddress]
    string email;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string direccion;

    [ObservableProperty]
    bool visibleError;

    [ObservableProperty]
    bool visibleAgregar = true;

    [RelayCommand]
    private async void Agregar()
    {
        if (await Guardar())
        {
            Nombre = string.Empty;
            Nit = string.Empty;
            Telefono = string.Empty;
            Email = string.Empty;
            Direccion = string.Empty;
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

        var newProveedor = new Proveedor(nombre, nit, telefono, email, direccion);
        var resul = WeakReferenceMessenger.Default.Send<Proveedor>(newProveedor);
        return resul is not null;
    }
    #endregion
}
