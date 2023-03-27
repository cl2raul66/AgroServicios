﻿using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgVentaVistaModelo : ObservableRecipient
{
    public PgVentaVistaModelo()
    {
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        WeakReferenceMessenger.Default.Register<PgVentaVistaModelo, VentaView>(this, (r, m) =>
        {
            if (m is not null)
            {
                Ventas.Insert(0, m);
            }
        });
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
    }

    [ObservableProperty]
    ObservableCollection<VentaView> ventas = new();

    [ObservableProperty]
    VentaView selectedVenta;

    [RelayCommand]
    private async Task Agregar()
    {
        await Shell.Current.GoToAsync($"{nameof(PgAgregarVenta)}");
    }

    [RelayCommand]
    private void Eliminar()
    {
        Ventas.Remove(selectedVenta);
    }
}
