using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgVentasVistaModelo : ObservableRecipient
{
    readonly IVentasRepositorio ventasServ;
    readonly IInventarioRepositorio inventarioServ;

    public PgVentasVistaModelo(IVentasRepositorio ventasRepositorio, IInventarioRepositorio inventarioRepositorio)
    {
        IsActive = true;

        ventasServ = ventasRepositorio;
        inventarioServ = inventarioRepositorio;

        GetVentas();

    }

    [ObservableProperty]
    ObservableCollection<VentaView> ventas;

    [ObservableProperty]
    bool enableVeragregarventa;

    [RelayCommand]
    async Task VerAgregarVenta()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarVenta), true);
    }

    #region Extra
    protected override void OnActivated()
    {
        base.OnActivated();
        WeakReferenceMessenger.Default.Register<PgVentasVistaModelo, Venta>(this, (r, m) =>
        {
            if (m is not null)
            {
                ventasServ.Insert(m);
                GetVentas();
            }
        });
    }

    void GetVentas()
    {
        Ventas = ventasServ.AnyVenta
            ? new(ventasServ.GetAll.Select(x => new VentaView(x)))
            : null;

        EnableVeragregarventa = inventarioServ.AnyInventario;

        //var r = WeakReferenceMessenger.Default.Send<object, string>(Ventas is not null && Ventas.Count > 0, nameof(EnableVeragregarventa));
        //if (r is bool v)
        //{
        //    EnableVeragregarventa = v;
        //}
    }
    #endregion
}
