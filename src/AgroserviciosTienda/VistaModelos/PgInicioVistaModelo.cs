using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgInicioVistaModelo : ObservableRecipient
{
    protected override void OnActivated()
    {
        base.OnActivated();
    }

    [RelayCommand]
    async Task VerAddentrada()
    {
        //InventarioVM.IsActive = true;
        await Shell.Current.GoToAsync($"//{nameof(PgInventario)}/{nameof(PgAgregarEntrada)}", true);
    }

    [RelayCommand]
    async Task VerRealizarventa()
    {
        await Shell.Current.GoToAsync($"//{nameof(PgVentas)}/{nameof(PgAgregarVenta)}", true);
    }
}
