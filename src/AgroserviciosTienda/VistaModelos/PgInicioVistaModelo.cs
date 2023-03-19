using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas.Entradas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgInicioVistaModelo : ObservableObject
{
    [RelayCommand]
    private async Task VerEntradas()
    {
        await Shell.Current.GoToAsync(nameof(PgEntDetalles));
    }

    [RelayCommand]
    private async Task AgregarEntrada()
    {
        await Shell.Current.GoToAsync($"{nameof(PgEntDetalles)}/{nameof(PgEntAddEdit)}", new Dictionary<string, object>() { { "entrada", null as object } });
    }

    [RelayCommand]
    private async Task VerVentas()
    {
        await Shell.Current.GoToAsync(nameof(PgVenDetalles));
    }

    [RelayCommand]
    private async Task AgregarVenta()
    {
        await Shell.Current.GoToAsync($"{nameof(PgVenDetalles)}/{nameof(PgVenAddEdit)}", new Dictionary<string, object>() { { "entrada", null as object } });
    }
}
