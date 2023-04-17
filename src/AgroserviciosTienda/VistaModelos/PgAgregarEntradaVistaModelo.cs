using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(CurrentEntrada), "entrada")]
public partial class PgAgregarEntradaVistaModelo : ObservableValidator
{
    public PgAgregarEntradaVistaModelo(IProveedoresRepositorio proveedores)
    {
        Proveedores = new(proveedores.GetAll());

        WeakReferenceMessenger.Default.Register<PgAgregarEntradaVistaModelo, Producto>(this, (r, m) =>
        {
            if (m is not null)
            {
                var idx = productos.IndexOf(productos.FirstOrDefault(x => x.Nombre == m.Nombre));
                if (idx > -1)
                {
                    Productos[idx] = m;
                }
                else
                {
                    Productos.Insert(0, m);
                }
            }
        });
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    Entrada currentEntrada;

    public string Titulo => CurrentEntrada is null ? "Entrada - Nuevo" : "Entrada - Modificar";

    [ObservableProperty]
    DateTime fecha = DateTime.Now;

    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    Contacto selectedProveedor;

    [ObservableProperty]
    ObservableCollection<Contacto> proveedores;

    [ObservableProperty]
    decimal costoFlete;

    [ObservableProperty]
    decimal costoCarga;

    [ObservableProperty]
    bool visibleError;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentEntrada))
        {
            if (CurrentEntrada is not null)
            {
                Fecha = CurrentEntrada.Fecha;
                Productos = new(CurrentEntrada.Productos);
                NoFactura = CurrentEntrada.NoFactura;
                SelectedProveedor = CurrentEntrada.Proveedor;
                CostoFlete = CurrentEntrada.CostoFlete;
                CostoCarga = CurrentEntrada.CostoCarga;
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

        Entrada newEntrada = new(Fecha, Productos.ToList(), NoFactura, SelectedProveedor, CostoFlete, CostoCarga);

        //entradasServ.Insert(newEntrada);

        WeakReferenceMessenger.Default.Send<Entrada>(newEntrada);
        await Cancelar();
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task AgregarProveedor()
    {
        Tuple<Contacto, bool, bool> contactodatosnav = new(SelectedProveedor, false, false);
        await Shell.Current.GoToAsync($"{nameof(PgContactoAddEdit)}", parameters: new Dictionary<string, object>() { { "contactodatosnav", contactodatosnav } });
    }

    #region productos
    [ObservableProperty]
    [Required]
    [MinLength(1)]
    ObservableCollection<Producto> productos = new();

    [ObservableProperty]
    Producto selectedProducto;

    [RelayCommand]
    async Task AgregarModificarProducto()
    {
        Tuple<Producto, bool> productodatosNav = new(SelectedProducto, false);
        await Shell.Current.GoToAsync($"{nameof(PgProductoAddEdit)}", new Dictionary<string, object>() { { "productodatosNav", productodatosNav } });
    }

    [RelayCommand]
    private void EliminarProducto()
    {
        Productos.Remove(SelectedProducto);
    }
    #endregion
}
