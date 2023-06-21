using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgAgregarEntradaVistaModelo : ObservableValidator
{
    readonly IContactosRepositorio<Proveedor> proveedoresServ;
    readonly IInventarioRepositorio inventarioServ;
    readonly IEntradasRepositorio entradasServ;

    public PgAgregarEntradaVistaModelo(IContactosRepositorio<Proveedor> contactosRepositorio, IInventarioRepositorio inventarioRepositorio, IEntradasRepositorio entradasRepositorio)
    {
        proveedoresServ = contactosRepositorio;
        inventarioServ = inventarioRepositorio;
        entradasServ = entradasRepositorio;

        if (proveedoresServ.AnyContacto)
        {
            Proveedores = new(proveedoresServ.GetAll);
        }

        if (inventarioServ.AnyInventario)
        {
            ProductosPicker = new(inventarioServ.GetAll.Select(x => x.Articulo));
        }

        WeakReferenceMessenger.Default.Register<PgAgregarEntradaVistaModelo, Proveedor>(this, (r, m) =>
        {
            if (m is not null)
            {
                Proveedores.Insert(0, m);
                SelectedProveedor = Proveedores[0];
            }
        });

        WeakReferenceMessenger.Default.Register<PgAgregarEntradaVistaModelo, ProductoEntrada>(this, (r, m) =>
        {
            if (m is not null)
            {
                ProductosPicker.Insert(0, m.Articulo);
                AddProductoEntradaToLista(new(m.Articulo, m.CantidadUnidad, m.Precio));
            }
        });
        ValidateAllProperties();
    }

    [ObservableProperty]
    DateTime fecha = DateTime.Now;

    #region Con Factura
    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    ObservableCollection<Proveedor> proveedores = new();

    [ObservableProperty]
    Proveedor selectedProveedor;

    [ObservableProperty]
    decimal costoFlete;

    [ObservableProperty]
    decimal costoCarga;

    [RelayCommand]
    private async Task VerAgregarproveedor()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarProveedor), true);
    }
    #endregion

    #region Productos
    [ObservableProperty]
    ObservableCollection<Producto> productosPicker = new();

    [ObservableProperty]
    Producto selectedProductopicker;

    [ObservableProperty]
    int cantidadunidad;

    [ObservableProperty]
    double precio;

    [ObservableProperty]
    double costoEntrada;

    [RelayCommand]
    async Task VerAgregarproductosentrada()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarProductosEntrada), true);
    }
    #endregion

    #region Productos Entradas
    [ObservableProperty]
    [Required]
    [MinLength(1)]
    ObservableCollection<ProductoEntrada> productosLista = null;

    [ObservableProperty]
    ProductoEntrada currentproductoLista;

    [ObservableProperty]
    bool enableAgregarproductotolista;

    [RelayCommand]
    async Task AgregarProductoToLista()
    {
        AddProductoEntradaToLista();
        await Task.CompletedTask;
    }

    [RelayCommand]
    async Task EliminarProductoToLista()
    {
        ProductosLista.Remove(CurrentproductoLista);
        CurrentproductoLista = null;
        GetCostoTotal();
        await Task.CompletedTask;
    }
    #endregion

    [RelayCommand]
    async Task Guardar()
    {
        if (string.IsNullOrEmpty(NoFactura))
        {
            entradasServ.Insert(new(Fecha, ProductosLista.ToList()));
        }
        else
        {
            proveedoresServ.Insert(SelectedProveedor);
            entradasServ.Insert(new(Fecha, ProductosLista.ToList(), NoFactura, SelectedProveedor, CostoFlete, CostoCarga));
        }

        bool seAgrego = false;
        foreach (var item in ProductosLista)
        {
            Inventario newInventario = item.Articulo.Presentacion?.Valor == 0
            ? new(item.Articulo, item.CantidadUnidad)
            : new(item.Articulo, item.CantidadUnidad * item.Articulo.Presentacion.Valor);

            inventarioServ.Upset(newInventario);

            seAgrego = true;
        }

        WeakReferenceMessenger.Default.Send(seAgrego.ToString());

        await Cancelar();
    }

    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    #region Extra
    void GetCostoTotal()
    {
        CostoEntrada = ProductosLista?.Select(x => x.CantidadUnidad * x.Precio).Sum() ?? 0;
        ValidateAllProperties();
    }

    void AddProductoEntradaToLista(ProductoEntrada newProductoentrada = null)
    {
        newProductoentrada ??= new(SelectedProductopicker, Cantidadunidad, Precio);

        if (ProductosLista?.Any() ?? false)
        {
            ProductosLista.Insert(0, newProductoentrada);
        }
        else
        {
            ProductosLista = new() { newProductoentrada };
        }

        var cacheArticulo = new ProductoEntradaArticuloCache(
            newProductoentrada.CantidadUnidad,
            newProductoentrada.Precio
        );

        string articulo = $"Cache_{newProductoentrada.Articulo.ToString().Replace(' ', '%')}";

        Preferences.Default.Set(articulo, JsonSerializer.Serialize(cacheArticulo));

        if (SelectedProductopicker is not null)
        {
            SelectedProductopicker = null;
            Precio = 0;
            Cantidadunidad = 0;
        }

        GetCostoTotal();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SelectedProductopicker))
        {
            if (SelectedProductopicker is not null)
            {
                string articulo = $"Cache_{SelectedProductopicker.ToString().TrimEnd().Replace(' ', '%')}";
                string cacheArticuloJson = Preferences.Default.Get(articulo, string.Empty);
                if (!string.IsNullOrEmpty(cacheArticuloJson))
                {
                    var cacheArticulo = JsonSerializer.Deserialize<ProductoEntradaArticuloCache>(cacheArticuloJson);

                    Cantidadunidad = cacheArticulo.CantidadUnidad;
                    Precio = cacheArticulo.Precio;
                }
            }

            EnableAgregarproductotolista = Precio > 0 && Cantidadunidad > 0 && SelectedProductopicker is not null;
        }
        base.OnPropertyChanged(e);
    }
    #endregion
}
