using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public PgAgregarEntradaVistaModelo()
    {
        ValidateAllProperties();
    }

    [ObservableProperty]
    DateTime fecha = DateTime.Now;

    #region Con Factura
    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    ObservableCollection<object> proveedores;

    [ObservableProperty]
    object selectedProveedor;

    [ObservableProperty]
    decimal costoFlete;

    [ObservableProperty]
    decimal costoCarga;

    [RelayCommand]
    private async Task VerAgregarproveedor()
    {
        await Cancelar();
    }
    #endregion

    #region Productos
    [ObservableProperty]
    [Required]
    [MinLength(1)]
    ObservableCollection<object> productos = new();

    [ObservableProperty]
    object selectedProducto;

    [RelayCommand]
    private async Task VerAgregarproductosentrada()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarProductosEntrada), true);
    }
    #endregion

    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..", true);
    }
}
