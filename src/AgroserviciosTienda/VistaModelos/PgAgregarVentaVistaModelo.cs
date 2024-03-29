﻿using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Servicios;
using AgroserviciosTienda.Vistas;
using Android.Telephony;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
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
    Inventario selectedProductopicker;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentExistencia))]
    bool ventaAgranel = false;

    [ObservableProperty]
    ObservableCollection<TipoUnidad> tiposUnidad;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentExistencia))]
    TipoUnidad selectedTipounidad;

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

    public double CurrentExistencia
    {
        get
        {
            double resul = 0;
            var i = inventarioServ.GetAll.FirstOrDefault(x => x.ToString() == (SelectedProductopicker?.ToString() ?? string.Empty));
            // nota: en caso que VentaAgranel es true, cambiar i.Existencia ya que esa no es la operación correcta
            if (i is not null)
            {
                resul = i.Existencia;
                if (!VentaAgranel)
                {
                    resul = i.Existencia / i.Articulo.Presentacion.Valor;
                }
                else if (SelectedTipounidad is not null && !i.Articulo.Presentacion.Unidad.Equals(SelectedTipounidad.Abreviatura))
                {
                    resul = medidasServ.ConvertirExistencia(
                        i.Articulo.Presentacion.Medida,
                        i.Articulo.Presentacion.Unidad,
                        i.Existencia,
                        SelectedTipounidad.Nombre
                        );
                }
            }
            return resul;
        }
    }

    [RelayCommand]
    async Task AgregarProductoToLista()
    {
        if (CurrentExistencia > (string.IsNullOrEmpty(Cantidad) ? 0 : int.Parse(Cantidad)))
        {
            AddProductoVentaToLista();
        }

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
        ImporteVenta = ProductosLista?.Sum(x => x.CantidadUnidad * x.Precio) ?? 0.00;

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

        if (SelectedProductopicker is not null)
        {
            SelectedProductopicker = null;
            Precio = null;
            Cantidad = null;
        }

        GetCostoTotal();
    }

    void GetEnableagregarproductotolista()
    {
        var c = string.IsNullOrEmpty(Cantidad) ? 0 : int.Parse(Cantidad);
        EnableAgregarproductotolista = SelectedProductopicker is not null
            && (string.IsNullOrEmpty(Precio) ? 0 : double.Parse(Precio)) > 0
            && c <= CurrentExistencia && c > 0;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(SelectedProductopicker))
        {
            if (SelectedProductopicker?.PrecioInicial > 0)
            {
                Precio = SelectedProductopicker.PrecioInicial.ToString();
            }

            TiposUnidad = SelectedProductopicker is not null
                ? new(medidasServ.AllUnidades(SelectedProductopicker.Articulo.Presentacion.Medida))
                : new();
        }

        if (e.PropertyName == nameof(TiposUnidad))
        {
            if (TiposUnidad?.Count > 0)
            {
                SelectedTipounidad = tiposUnidad.FirstOrDefault(x => x.Abreviatura == selectedProductopicker.Articulo.Presentacion.Unidad);
            }
        }

        if (e.PropertyName == nameof(Cantidad))
        {
            GetEnableagregarproductotolista();
        }

        if (e.PropertyName == nameof(Precio))
        {
            GetEnableagregarproductotolista();
        }
    }
    #endregion
}
