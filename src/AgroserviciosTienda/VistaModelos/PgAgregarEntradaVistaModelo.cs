using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            ProveedoresPicker = new(proveedoresServ.GetAll);
        }

        if (inventarioServ.AnyInventario)
        {
            ProductosPicker = new(inventarioServ.GetAll.Select(x => x.Articulo));
        }

        WeakReferenceMessenger.Default.Register<PgAgregarEntradaVistaModelo, Proveedor>(this, (r, m) =>
        {
            if (m is not null)
            {
                ProveedoresPicker.Insert(0, m);
                SelectedProveedorpicker = ProveedoresPicker[0];
            }
        });

        WeakReferenceMessenger.Default.Register<PgAgregarEntradaVistaModelo, ProductoEntrada>(this, (r, m) =>
        {
            if (m is not null)
            {
                ProductosPicker.Insert(0, m.Articulo);
                AddProductoEntradaToLista(new(m.Articulo, m.CantidadUnidad, m.Precio));
                Precio = null;
                Cantidadunidad = null;
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
    ObservableCollection<Proveedor> proveedoresPicker = new();

    [ObservableProperty]
    Proveedor selectedProveedorpicker;

    [ObservableProperty]
    string costoFlete;

    [ObservableProperty]
    string costoCarga;

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
    string cantidadunidad;

    [ObservableProperty]
    string precio;

    [ObservableProperty]
    double costoEntrada;

    [RelayCommand]
    async Task VerAgregarproductosentrada()
    {
        var p = string.IsNullOrEmpty(Precio) ? 0.00 : double.Parse(Precio);
        var cu = string.IsNullOrEmpty(Cantidadunidad) ? 0 : int.Parse(Cantidadunidad);
        if (p > 0 || cu > 0)
        {
            await Shell.Current.GoToAsync(nameof(PgAgregarProductosEntrada), true,
                new Dictionary<string, object>() {
                    { nameof(Precio), p },
                    { nameof(Cantidadunidad), cu }
                });
        }
        else
        {
            await Shell.Current.GoToAsync(nameof(PgAgregarProductosEntrada), true);
        }

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
            proveedoresServ.Insert(SelectedProveedorpicker);
            entradasServ.Insert(
                new(
                    Fecha,
                    ProductosLista.ToList(),
                    NoFactura,
                    SelectedProveedorpicker,
                    string.IsNullOrEmpty(CostoFlete) ? 0 : decimal.Parse(CostoFlete),
                    string.IsNullOrEmpty(CostoCarga) ? 0 : decimal.Parse(CostoCarga)
                ));
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
        CostoEntrada = (ProductosLista?.Sum(x => x.CantidadUnidad * x.Precio) ?? 0.00)
            + (string.IsNullOrEmpty(CostoFlete) ? 0.00 : double.Parse(CostoFlete))
            + (string.IsNullOrEmpty(CostoCarga) ? 0.00 : double.Parse(CostoCarga));

        ValidateAllProperties();
    }

    void AddProductoEntradaToLista(ProductoEntrada newProductoentrada = null)
    {
        newProductoentrada ??= new(
            SelectedProductopicker,
            string.IsNullOrEmpty(Cantidadunidad) ? 0 : int.Parse(Cantidadunidad),
            string.IsNullOrEmpty(Precio) ? 0.00 : double.Parse(Precio));

        if (ProductosLista?.Any() ?? false)
        {
            ProductosLista.Insert(0, newProductoentrada);
        }
        else
        {
            ProductosLista = new() { newProductoentrada };
        }

        if (SelectedProductopicker is not null)
        {
            SelectedProductopicker = null;
            Precio = null;
            Cantidadunidad = null;
        }

        GetCostoTotal();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(SelectedProductopicker))
        {
            if (SelectedProductopicker is not null)
            {
                var pEntrada = entradasServ.GetProductoentrada(SelectedProductopicker);

                if (string.IsNullOrEmpty(Cantidadunidad))
                {
                    Cantidadunidad = pEntrada?.CantidadUnidad.ToString() ?? string.Empty;
                }
                if (string.IsNullOrEmpty(Precio))
                {
                    Precio = pEntrada?.Precio.ToString() ?? string.Empty;
                }
            }

            EnableAgregarproductotolista = SelectedProductopicker is not null
                && (string.IsNullOrEmpty(Precio) ? 0 : double.Parse(Precio)) > 0
                && (string.IsNullOrEmpty(Cantidadunidad) ? 0 : int.Parse(Cantidadunidad)) > 0;
        }

        if (e.PropertyName == nameof(CostoFlete))
        {
            GetCostoTotal();
        }

        if (e.PropertyName == nameof(CostoCarga))
        {
            GetCostoTotal();
        }
    }
    #endregion
}
