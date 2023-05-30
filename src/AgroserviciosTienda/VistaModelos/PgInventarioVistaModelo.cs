using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
