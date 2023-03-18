using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas.Entradas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos.Entradas;

public partial class PgEntDetallesVistaModelo : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<EntradaView> entradas;

    [ObservableProperty]
    EntradaView? selectedEntrada;

    [RelayCommand]
    private async Task AgregarEntrada()
    {
        await Shell.Current.GoToAsync($"{nameof(PgEntDetalles)}/{nameof(PgEntAddEdit)}", new Dictionary<string, object>() { { "t", true as object } });
    }

    [RelayCommand]
    private async Task ModificarEntrada()
    {
        await Shell.Current.GoToAsync($"{nameof(PgEntDetalles)}/{nameof(PgEntAddEdit)}", new Dictionary<string, object>() { { "t", false as object } });
    }
}
