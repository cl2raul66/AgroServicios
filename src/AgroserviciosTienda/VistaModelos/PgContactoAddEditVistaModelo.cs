using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(DatosNav), "contactodatosnav")]
public partial class PgContactoAddEditVistaModelo : ObservableValidator
{
    readonly IContactosRepositorio<Proveedor> proveedoresServ;

    public PgContactoAddEditVistaModelo(IContactosRepositorio<Proveedor> proveedores)
    {
        proveedoresServ = proveedores;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(DatosNav))
        {
            if (DatosNav is not null)
            {
                CurrentContacto = DatosNav.Item1;
                VisibleAgregar = DatosNav.Item2;
                Titulo = $"{(DatosNav.Item3 ? "Cliente" : "Proveedor")} - {(DatosNav.Item1 is null ? "Nuevo" :"Modificar" )}";
            }
        }

        if (e.PropertyName == nameof(CurrentContacto))
        {
            if (CurrentContacto is not null)
            {
                Nombre = CurrentContacto.Nombre;
                Nit = CurrentContacto.Nit;
                Telefono = CurrentContacto.Telefono;
                Email = CurrentContacto.EMail;
                Direccion = CurrentContacto.Direccion;
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

        var newContacto = new Proveedor(Nombre, Nit, Telefono, Email, Direccion, EsEmpresa);
        //var resul = WeakReferenceMessenger.Default.Send<Contacto>(newContacto);
        proveedoresServ.Insert(newContacto);

        return true;
    }
}
