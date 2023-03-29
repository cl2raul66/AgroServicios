﻿using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(CurrentProducto), "producto")]
public partial class PgProductosAddEditVistaModelo : ObservableValidator
{
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentProducto))
        {
            if (currentProducto is not null)
            {
                ProductoNombre = currentProducto.Nombre;
                Cantidad = currentProducto.Cantidad;
                Precio = currentProducto.Precio;
            }
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    Producto currentProducto;

    public string Titulo => currentProducto is null ? "Nueva - Producto" : "Modificar - Producto";

    [ObservableProperty]
    public List<string> productosNombre;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string productoNombre;

    [ObservableProperty]
    [Required]
    [Range(1, 1000)]
    int cantidad;

    [ObservableProperty]
    [Required]
    [Range(1, 19999.99)]
    decimal precio;

    [ObservableProperty]
    bool visibleError;

    [RelayCommand]
    private async void Agregar()
    {
        if (await Guardar())
        {
            ProductoNombre = string.Empty;
            Cantidad = 0;
            Precio = 0;
        }
    }

    [RelayCommand]
    private async void AgregarSalir()
    {
        if (await Guardar())
        {
            await Cancelar();
        }
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }

    #region extra
    async Task<bool> Guardar()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return false;
        }

        var newProucto = new Producto(productoNombre, (int)cantidad, (decimal)precio);
        var resul = WeakReferenceMessenger.Default.Send<Producto>(newProucto);
        return resul is not null;
    }
    #endregion
}
