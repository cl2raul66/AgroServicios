using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas.Entradas;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AgroserviciosTienda.VistaModelos.Entradas;

public partial class PgEntDetallesVistaModelo : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<EntradaView> entradas;

    [ObservableProperty]
    EntradaView? selectedEntrada;

    [RelayCommand]
    private async Task Agregar()
    {
        await Shell.Current.GoToAsync($"{nameof(PgEntDetalles)}/{nameof(PgEntAddEdit)}", new Dictionary<string, object>() { { "entrada", selectedEntrada as object } });
    }

    [RelayCommand]
    private async Task Modificar()
    {
        await Shell.Current.GoToAsync($"{nameof(PgEntDetalles)}/{nameof(PgEntAddEdit)}", new Dictionary<string, object>() { { "entrada", selectedEntrada as object } });
    }

    [RelayCommand]
    private async Task Eliminar()
    {
        bool resul = Entradas.Remove(selectedEntrada);

        CancellationTokenSource cancellationTokenSource = new();
        string text = resul ? "Se elimino correctamente" : "No se pudo eliminar";
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
    }
}
