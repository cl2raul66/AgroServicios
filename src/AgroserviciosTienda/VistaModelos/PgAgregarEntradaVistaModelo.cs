using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgAgregarEntradaVistaModelo : ObservableValidator
{
    readonly IContactosRepositorio<Proveedor> proveedoresServ;
    readonly IEntradasRepositorio entradasServ;

    public PgAgregarEntradaVistaModelo(IContactosRepositorio<Proveedor> contactosRepositorio, IEntradasRepositorio entradasRepositorio)
    {
        proveedoresServ = contactosRepositorio;
        entradasServ = entradasRepositorio;

        ValidateAllProperties();

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
                Productos.Insert(0, m);
                SelectedProducto = Productos[0];
                _ = AgregarProductoToLista();
            }
        });
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

    #region Productos Entradas
    [ObservableProperty]
    [Required]
    [MinLength(1)]
    ObservableCollection<ProductoEntrada> productosLista = null;

    [ObservableProperty]
    double costoEntrada;

    [ObservableProperty]

    ProductoEntrada currentproductoLista;

    [ObservableProperty]
    ObservableCollection<ProductoEntrada> productos = new();

    [ObservableProperty]
    ProductoEntrada selectedProducto;

    [RelayCommand]
    async Task VerAgregarproductosentrada()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarProductosEntrada), true);
    }

    [RelayCommand]
    async Task AgregarProductoToLista()
    {
        if (ProductosLista?.Any() ?? false)
        {
            ProductosLista.Insert(0, SelectedProducto);
        }
        else
        {
            ProductosLista = new() { SelectedProducto };
        }
        SelectedProducto = null;
        GetCostoTotal();
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
        
        await Cancelar();
    }

    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    #region calcular costo total
    void GetCostoTotal()
    {
        CostoEntrada = ProductosLista?.Select(x => x.CantidadUnidad * x.Precio).Sum() ?? 0; ValidateAllProperties();
        ValidateAllProperties();
    }
    #endregion
}
