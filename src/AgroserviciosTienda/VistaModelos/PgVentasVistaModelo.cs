using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgVentasVistaModelo : ObservableRecipient
{
    readonly IVentasRepositorio ventasServ;

    public PgVentasVistaModelo(IVentasRepositorio ventasRepositorio)
    {
        IsActive = true;

        ventasServ = ventasRepositorio;
        GetVentas();
    }

    [ObservableProperty]
    ObservableCollection<VentaView> ventas;

    async Task VerAgregarVenta()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarVenta), true);
    }

    #region Extra
    protected override void OnActivated()
    {
        base.OnActivated();
        WeakReferenceMessenger.Default.Register<PgVentasVistaModelo,Venta>(this, (r, m) => {
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
    }
    #endregion
}
