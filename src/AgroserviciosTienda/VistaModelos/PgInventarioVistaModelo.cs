using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgInventarioVistaModelo : ObservableRecipient
{
    protected override void OnActivated()
    {
        base.OnActivated();
    }

    [RelayCommand]
    async Task VerAgregarentrada()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarEntrada), true);
    }
}
