using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas.Entradas;
using AgroserviciosTienda.Vistas.Ventas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AgroserviciosTienda.VistaModelos.Ventas;

public partial class PgVenDetallesVistaModelo : ObservableRecipient
{
    public PgVenDetallesVistaModelo()
    {
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        WeakReferenceMessenger.Default.Register<PgVenDetallesVistaModelo, VentaView>(this, (r, m) =>
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
        await Shell.Current.GoToAsync($"{nameof(PgVenAddEdit)}");
    }

    private object PgVenAddEdit()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Modificar()
    {
        await Shell.Current.GoToAsync($"{nameof(PgVenAddEdit)}", new Dictionary<string, object>() { { "entrada", selectedVenta } });
    }

    [RelayCommand]
    private void Eliminar()
    {
        Ventas.Remove(selectedVenta);
    }
}
