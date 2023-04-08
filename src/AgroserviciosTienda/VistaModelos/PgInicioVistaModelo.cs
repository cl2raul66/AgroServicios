using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgInicioVistaModelo : ObservableObject
{
    //[RelayCommand]
    //private async Task VerAjustess()
    //{
    //    await Shell.Current.GoToAsync($"//{nameof(PgAjustes)}");
    //}

    [RelayCommand]
    private async Task VerEntradas()
    {
        await Shell.Current.GoToAsync($"//{nameof(PgEntrada)}");
    }

    [RelayCommand]
    private async Task AgregarEntrada()
    {
        await Shell.Current.GoToAsync($"//{nameof(PgEntrada)}/{nameof(PgAgregarEntrada)}");
    }

    [RelayCommand]
    private async Task AgregarVenta()
    {
        await Shell.Current.GoToAsync($"//{nameof(PgVenta)}/{nameof(PgAgregarVenta)}");
    }

    [RelayCommand]
    private async Task AgregarVentaConFacturacion()
    {
        await Shell.Current.GoToAsync($"//{nameof(PgVenta)}/{nameof(PgAgregarVenta)}");
    }
}
