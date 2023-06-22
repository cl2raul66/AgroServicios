using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgAgregarProveedorVistaModelo : ObservableValidator
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GuardarCommand))]
    [Required]
    [MinLength(3)]
    string nombre;

    [ObservableProperty]
    bool esEmpresa;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GuardarCommand))]
    [Required]
    [MinLength(6)]
    string nit;

    [ObservableProperty]
    [Phone]
    string telefono;

    [ObservableProperty]
    [EmailAddress]
    string email;

    [ObservableProperty]
    string direccion;

    [ObservableProperty]
    bool visibleError;

    [RelayCommand]
    async Task Guardar()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return;
        }

        var newProveedor = new Proveedor(
            Nombre.TrimEnd(),
            Nit.TrimEnd(),
            string.IsNullOrEmpty(Telefono) ? string.Empty : Telefono.TrimEnd(),
            string.IsNullOrEmpty(Email) ? string.Empty : Email.TrimEnd(), 
            string.IsNullOrEmpty(Direccion) ? string.Empty : Direccion.TrimEnd(), 
            EsEmpresa);

        var resul = WeakReferenceMessenger.Default.Send(newProveedor);
        if (resul is not null)
        {
            await Cancelar();
        }
    }

    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..", true);
    }
}
