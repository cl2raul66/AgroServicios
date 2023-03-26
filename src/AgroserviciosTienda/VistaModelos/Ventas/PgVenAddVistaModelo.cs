﻿using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos.Ventas;

[QueryProperty(nameof(CurrentVenta), "venta")]
public partial class PgVenAddVistaModelo : ObservableValidator
{
    public PgVenAddVistaModelo()
    {
        WeakReferenceMessenger.Default.Register<PgVenAddVistaModelo, Producto>(this, (r, m) =>
        {
            if (m is not null)
            {
                Productos.Insert(0, m);
            }
        });
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentVenta))
        {
            if (currentVenta is not null)
            {
                Fecha = currentVenta.Fecha;
                Productos = new(currentVenta.Productos);
            }
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    VentaView currentVenta;

    public string Titulo => currentVenta is null ? "Nueva - Venta" : "Modificar - Venta";

    [ObservableProperty]
    [Required]
    DateTime fecha = DateTime.Now;

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

        var newVenta = new VentaView(fecha, productos.ToList());
        WeakReferenceMessenger.Default.Send<VentaView>(newVenta);
        await Cancelar();
    }

    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }

    #region productos
    [ObservableProperty]
    [Required]
    [MinLength(1)]
    ObservableCollection<Producto> productos = new();

    [ObservableProperty]
    Producto selectedProducto;

    [RelayCommand]
    async Task AgregarProducto()
    {
        await Shell.Current.GoToAsync($"{nameof(PgProductosAddEdit)}");
    }

    [RelayCommand]
    async Task ModificarProducto()
    {
        await Shell.Current.GoToAsync($"{nameof(PgProductosAddEdit)}", new Dictionary<string, object>() { { "producto", selectedProducto } });
    }

    [RelayCommand]
    private void EliminarProducto()
    {
        productos.Remove(selectedProducto);
    }
    #endregion
}