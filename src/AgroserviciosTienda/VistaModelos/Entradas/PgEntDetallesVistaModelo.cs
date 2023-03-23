using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas.Entradas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AgroserviciosTienda.VistaModelos.Entradas;

public partial class PgEntDetallesVistaModelo : ObservableRecipient
{
    public PgEntDetallesVistaModelo()
    {
        IsActive = true;
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        WeakReferenceMessenger.Default.Register<PgEntDetallesVistaModelo, EntradaView>(this, (r, m) =>
        {
            if (m is not null)
            {
                Entradas.Add(m);
            }
        });
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
    }

    [ObservableProperty]
    ObservableCollection<EntradaView> entradas = new();

    [ObservableProperty]
    EntradaView selectedEntrada;

    [RelayCommand]
    private async Task Agregar()
    {
        await Shell.Current.GoToAsync($"{nameof(PgEntAddEdit)}");
    }

    [RelayCommand]
    private async Task Modificar()
    {
        await Shell.Current.GoToAsync($"{nameof(PgEntAddEdit)}", new Dictionary<string, object>() { { "entrada", selectedEntrada } });
    }

    [RelayCommand]
    private void Eliminar()
    {
        Entradas.Remove(selectedEntrada);
    }
}
