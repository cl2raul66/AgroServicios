using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Servicios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgAgregarVentaVistaModelo : ObservableValidator
{
    readonly IVentasRepositorio ventasServ;
    readonly IContactosRepositorio<Cliente> clientesServ;
    readonly IInventarioRepositorio inventarioServ;
    readonly IMedidasServicio medidasServ;

    public PgAgregarVentaVistaModelo(IVentasRepositorio ventasRepositorio, IContactosRepositorio<Cliente> clientesRepositorio, IInventarioRepositorio inventarioRepositorio, IMedidasServicio medidasServicio)
    {
        ventasServ = ventasRepositorio;
        clientesServ = clientesRepositorio;
        inventarioServ = inventarioRepositorio;
        medidasServ = medidasServicio;

        Productospicker = inventarioServ.AnyInventario
            ? new(inventarioServ.GetAll)
            : new();
    }

    [ObservableProperty]
    DateTime fecha = DateTime.Now;

    #region Con Factura
    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    ObservableCollection<Cliente> clientespicker = new();

    [ObservableProperty]
    Cliente selectedClientepicker;

    [RelayCommand]
    private async Task VerAgregarcliente()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarCliente), true);
    }
    #endregion

    #region Productos
    [ObservableProperty]
    ObservableCollection<Inventario> productospicker;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TiposUnidad))]
    Inventario selectedProductopicker;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TiposUnidad))]
    bool ventaAgranel = false;

    public ObservableCollection<TipoUnidad> TiposUnidad => 
        SelectedProductopicker is null && !VentaAgranel 
        ? new() 
        : new (medidasServ.AllUnidades(SelectedProductopicker.Articulo.Presentacion.Medida));

    [ObservableProperty]
    string cantidad;

    [ObservableProperty]
    string precio;

    [ObservableProperty]
    double importeVenta;
    #endregion

    #region Productos Ventas
    [ObservableProperty]
    [Required]
    [MinLength(1)]
    ObservableCollection<ProductoVenta> productosLista = null;

    [ObservableProperty]
    ProductoVenta currentproductoLista;

    [ObservableProperty]
    bool enableAgregarproductotolista;

    [RelayCommand]
    async Task AgregarProductoToLista()
    {
        AddProductoVentaToLista();
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
            ventasServ.Insert(new(Fecha, ProductosLista.ToList()));
        }
        else
        {
            clientesServ.Insert(SelectedClientepicker);
            ventasServ.Insert(
                new(
                    Fecha,
                    ProductosLista.ToList(),
                    NoFactura,
                    SelectedClientepicker
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
        ImporteVenta = (ProductosLista?.Sum(x => x.CantidadUnidad * x.Precio) ?? 0.00);

        ValidateAllProperties();
    }

    void AddProductoVentaToLista(ProductoVenta newProductoVenta = null)
    {
        newProductoVenta ??= new(
            SelectedProductopicker.Articulo,
            string.IsNullOrEmpty(Cantidad) ? 0 : int.Parse(Cantidad),
            string.IsNullOrEmpty(Precio) ? 0.00 : double.Parse(Precio));

        if (ProductosLista?.Any() ?? false)
        {
            ProductosLista.Insert(0, newProductoVenta);
        }
        else
        {
            ProductosLista = new() { newProductoVenta };
        }

        //var cacheArticulo = new ProductoEntradaArticuloCache(
        //    newProductoVenta.CantidadUnidad,
        //    newProductoVenta.Precio
        //);

        //string articulo = $"Cache_{newProductoentrada.Articulo.ToString().Replace(' ', '%')}";

        //Preferences.Default.Set(articulo, JsonSerializer.Serialize(cacheArticulo));

        if (SelectedProductopicker is not null)
        {
            SelectedProductopicker = null;
            Precio = null;
            Cantidad = null;
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
                //string articulo = $"Cache_{SelectedProductopicker.ToString().TrimEnd().Replace(' ', '%')}";
                //string cacheArticuloJson = Preferences.Default.Get(articulo, string.Empty);
                //if (!string.IsNullOrEmpty(cacheArticuloJson))
                //{
                //    var cacheArticulo = JsonSerializer.Deserialize<ProductoEntradaArticuloCache>(cacheArticuloJson);
                //    if (string.IsNullOrEmpty(Cantidadunidad))
                //    {
                //        Cantidadunidad = cacheArticulo.CantidadUnidad.ToString();
                //    }
                //    if (string.IsNullOrEmpty(Precio))
                //    {
                //        Precio = cacheArticulo.Precio.ToString();
                //    }

                //}

                Precio = SelectedProductopicker.PrecioInicial.ToString();
            }

            EnableAgregarproductotolista = SelectedProductopicker is not null
                && (string.IsNullOrEmpty(Precio) ? 0 : double.Parse(Precio)) > 0
                && (string.IsNullOrEmpty(Cantidad) ? 0 : int.Parse(Cantidad)) > 0;
        }
    }
    #endregion
}
