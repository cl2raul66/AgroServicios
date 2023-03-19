using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos.Entradas;

[QueryProperty(nameof(CurrentEntrada), "entrada")]
public partial class PgEntAddEditVistaModelo : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    EntradaView? currentEntrada;

    public string Titulo => currentEntrada is null ? "Nueva - Entrada" : "Modificar - Entrada";

    [ObservableProperty]
    DateTime fecha;

    [ObservableProperty]
    string producto;

    [ObservableProperty]
    double cantidad;

    [ObservableProperty]
    double precio;

    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    string proveedor;

    [ObservableProperty]
    double costoFlete;

    [ObservableProperty]
    double costoCarga;

    [RelayCommand]
    private async Task Guardar()
    {
        await Shell.Current.GoToAsync("..");
    }
    
    [RelayCommand]
    private async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }
}
