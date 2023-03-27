﻿using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgEntradaVistaModelo : ObservableRecipient
{
    public PgEntradaVistaModelo()
    {
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        WeakReferenceMessenger.Default.Register<PgEntradaVistaModelo, EntradaView>(this, (r, m) =>
        {
            if (m is not null)
            {
                Entradas.Insert(0, m);
            }
        });
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
    }

    [ObservableProperty]
    ObservableCollection<EntradaView> entradas = new();

    [RelayCommand]
    private async Task Agregar()
    {
        await Shell.Current.GoToAsync($"{nameof(PgAgregarEntrada)}");
    }
}