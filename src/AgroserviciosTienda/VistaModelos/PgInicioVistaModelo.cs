using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgInicioVistaModelo : ObservableRecipient
{
    readonly IInventarioRepositorio inventarioServ;

    public PgInicioVistaModelo(IInventarioRepositorio inventarioRepositorio)
    {
        IsActive = true;

        inventarioServ = inventarioRepositorio;
        EnableVeragregarventa = inventarioServ.AnyInventario;
    }

    [ObservableProperty]
    bool enableVeragregarventa;

    [RelayCommand]
    async Task VerAddentrada()
    {
        //InventarioVM.IsActive = true;
        await Shell.Current.GoToAsync($"//{nameof(PgInventario)}/{nameof(PgAgregarEntrada)}", true);
    }

    [RelayCommand]
    async Task VerRealizarventa()
    {
        await Shell.Current.GoToAsync($"//{nameof(PgVentas)}/{nameof(PgAgregarVenta)}", true);
    }

    #region Extra
    protected override void OnActivated()
    {
        base.OnActivated();

        WeakReferenceMessenger.Default.Register<PgInicioVistaModelo, object, string>(this, nameof(EnableVeragregarventa), (r, m) =>
        {
            if (m is not null and bool)
            {
                r.EnableVeragregarventa = (bool)m;
            }
        });
    }
    #endregion
}
