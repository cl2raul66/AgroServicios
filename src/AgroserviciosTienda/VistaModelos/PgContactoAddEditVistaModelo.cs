using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(DatosNav), "contacto")]
[QueryProperty(nameof(VisibleAgregar), "visibleagregar")]
public partial class PgContactoAddEditVistaModelo : ObservableValidator
{
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentContacto))
        {
            if (currentContacto is not null)
            {
                Nombre = currentContacto.Nombre;
                Nit = currentContacto.Nit;
                Telefono = currentContacto.Telefono;
                Email = currentContacto.EMail;
                Direccion = currentContacto.Direccion;
            }
        }
    }

    [ObservableProperty]
    Tuple<Contacto, bool> datosNav; //objeto Contacto y esCliente bool

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    Contacto currentContacto;

    public string Titulo => currentContacto is null ? "Nueva - Proveedor" : "Modificar - Proveedor";

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
