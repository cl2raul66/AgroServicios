using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgEstablecerPrecioInicialProductoVistaModelo : ObservableObject
{
    [ObservableProperty]
    decimal precioanterior;

    [ObservableProperty]
    decimal precioinicial;

    [ObservableProperty]
    string producto;

    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..", true);
    }
    
    [RelayCommand]
    async Task Guardar()
    {

        await Cancelar();
    }
}
