using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        var newProveedor = new Proveedor(Nombre, Nit, Telefono, Email, Direccion, EsEmpresa);

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
