using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(DatosNav), "contactodatosnav")]
public partial class PgContactoAddEditVistaModelo : ObservableValidator
{
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(DatosNav))
        {
            if (datosNav is not null)
            {
                CurrentContacto = datosNav.Item1;
                visibleAgregar = datosNav.Item2;
                Titulo = $"{(datosNav.Item3 ? "Cliente" : "Proveedor")} - {(datosNav.Item1 is null ? "Nuevo" :"Modificar" )}";
            }
        }

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
    Tuple<Contacto, bool, bool> datosNav; //objeto Contacto, visibleAgregar bool y esCliente bool

    [ObservableProperty]
    Contacto currentContacto;

    [ObservableProperty]
    public string titulo;

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
    bool esEmpresa;

    [ObservableProperty]
    bool visibleError;

    [ObservableProperty]
    bool visibleAgregar;

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
    private async void GuardarSalir()
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

    private async Task<bool> Guardar()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return false;
        }

        var newContacto = new Contacto(nombre, nit, telefono, email, direccion, esEmpresa);
        var resul = WeakReferenceMessenger.Default.Send<Contacto>(newContacto);
        return resul is not null;
    }
}
