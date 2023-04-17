using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgVentaVistaModelo : ObservableRecipient
{
    readonly IVentasRepositorio ventasServ;

    public PgVentaVistaModelo(IVentasRepositorio ventasRepo)
    {
        IsActive = true;
        ventasServ = ventasRepo;
        Ventas = ventasServ.AnyVenta ? new(ventasServ.GetAll) : new();
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        WeakReferenceMessenger.Default.Register<PgVentaVistaModelo, Venta>(this, (r, m) =>
        {
            if (m is not null)
            {
                ventasServ.Insert(m);
                Ventas.Insert(0, m);
            }
        });
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
    }

    [ObservableProperty]
    ObservableCollection<Venta> ventas;

    [ObservableProperty]
    Venta selectedVenta;

    [RelayCommand]
    private async Task Agregar()
    {
        await Shell.Current.GoToAsync($"{nameof(PgAgregarVenta)}");
    }

    [RelayCommand]
    private void Eliminar()
    {
        Ventas.Remove(SelectedVenta);
    }
}
