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

public partial class PgAgregarEntradaVistaModelo : ObservableValidator
{
    public PgAgregarEntradaVistaModelo(IContactosRepositorio<Proveedor> proveedores)
    {
        Proveedores = new(proveedores.GetAll());

        WeakReferenceMessenger.Default.Register<PgAgregarEntradaVistaModelo, ProductoEntrada>(this, (r, m) =>
        {
            if (m is not null)
            {
                var idx = productos.IndexOf(productos.FirstOrDefault(x => x.ElProducto.Nombre == m.ElProducto.Nombre));
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
    DateTime fecha = DateTime.Now;

    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    Contacto selectedProveedor = null;

    [ObservableProperty]
    ObservableCollection<Contacto> proveedores;

    [ObservableProperty]
    decimal costoFlete;

    [ObservableProperty]
    decimal costoCarga;

    [ObservableProperty]
    bool visibleError;

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
        //objeto Contacto, visibleAgregar bool y esCliente bool
        Tuple<Contacto, bool, bool> contactodatosnav = SelectedProveedor is null ? new(new(), Proveedores.Count > 0, false) : new(SelectedProveedor, Proveedores.Count > 0, false);

        await Shell.Current.GoToAsync($"{nameof(PgContactoAddEdit)}", new Dictionary<string, object>() { { "contactodatosnav", contactodatosnav } });
    }

    #region productos
    [ObservableProperty]
    [Required]
    [MinLength(1)]
    ObservableCollection<ProductoEntrada> productos = new();

    [ObservableProperty]
    ProductoEntrada selectedProducto;

    [RelayCommand]
    async Task AgregarModificarProducto()
    {
        await Shell.Current.GoToAsync($"{nameof(PgProductosEntradas)}", true);
    }

    [RelayCommand]
    private void EliminarProducto()
    {
        Productos.Remove(SelectedProducto);
    }
    #endregion
}
