using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(CurrentVenta), "venta")]
public partial class PgAgregarVentaVistaModelo : ObservableValidator
{
    public PgAgregarVentaVistaModelo()
    {
        WeakReferenceMessenger.Default.Register<PgAgregarVentaVistaModelo, Producto>(this, (r, m) =>
        {
            if (m is not null)
            {
                Productos.Insert(0, m);
            }
        });
        
        WeakReferenceMessenger.Default.Register<PgAgregarVentaVistaModelo, Contacto>(this, (r, m) =>
        {
            if (m is not null)
            {
                Clientes.Insert(0, m);
            }
        });
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentVenta))
        {
            if (CurrentVenta is not null)
            {
                Fecha = CurrentVenta.Fecha;
                Productos = new(CurrentVenta.Productos);
            }
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    Venta currentVenta;

    public string Titulo => CurrentVenta is null ? "Ventas - Nuevo" : "Venta - Modificar";

    [ObservableProperty]
    [Required]
    DateTime fecha = DateTime.Now;

    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    Contacto selectedCliente;

    [ObservableProperty]
    ObservableCollection<Contacto> clientes;

    [ObservableProperty]
    bool visibleError;

    [RelayCommand]
    async void Guardar()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return;
        }

        Venta newVenta = string.IsNullOrEmpty(NoFactura) ? new(Fecha, Productos.ToList()) : new(Fecha, Productos.ToList(), NoFactura, SelectedCliente);
        WeakReferenceMessenger.Default.Send<Venta>(newVenta);
        await Cancelar();
    }

    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task AgregarCliente()
    {
        Tuple<Contacto, bool, bool> contactodatosnav = new(SelectedCliente, false, true);
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
        Tuple<Producto, bool> productodatosNav = new(SelectedProducto, true);
        await Shell.Current.GoToAsync($"{nameof(PgAddProductos)}", new Dictionary<string, object>() { { "productodatosNav", productodatosNav } });
    }

    [RelayCommand]
    private void EliminarProducto()
    {
        Productos.Remove(SelectedProducto);
    }
    #endregion
}
