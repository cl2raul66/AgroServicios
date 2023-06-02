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

    public PgAgregarEntradaVistaModelo(IContactosRepositorio<Proveedor> contactosRepositorio)
    {
        proveedoresServ = contactosRepositorio;

        WeakReferenceMessenger.Default.Register<PgAgregarEntradaVistaModelo, Proveedor>(this, (r, m) =>
        {
            if (m is not null)
            {
                Proveedores.Insert(0, m);
                SelectedProveedor = Proveedores[0];
            }
        });

        WeakReferenceMessenger.Default.Register<PgAgregarEntradaVistaModelo, Producto>(this, (r, m) =>
        {
            if (m is not null)
            {
                Productos.Insert(0, m);
                SelectedProducto = Productos[0];
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

    #region Productos
    [ObservableProperty]
    [Required]
    [MinLength(1)]
    ObservableCollection<Producto> productos = new();

    [ObservableProperty]
    Producto selectedProducto;

    [RelayCommand]
    private async Task VerAgregarproductosentrada()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarProductosEntrada), true);
    }
    #endregion

    [RelayCommand]
    async Task Guardar()
    {
        proveedoresServ.Insert(SelectedProveedor);

        await Cancelar();
    }
    
    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..", true);
    }
}
