using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(CurrentInventario), "inventario")]
public partial class PgEstablecerPrecioInicialProductoVistaModelo : ObservableValidator
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Precioanterior))]
    [NotifyPropertyChangedFor(nameof(Producto))]
    Inventario currentInventario;

    public decimal Precioanterior => CurrentInventario?.PrecioInicial ?? 0;

    [ObservableProperty]
    [Required]
    [Range(1, 999.99)]
    decimal precioinicial;

    public string Producto => CurrentInventario?.Articulo.ToString() ?? "None";

    [ObservableProperty]
    bool visibleError = false;

    [RelayCommand]
    async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    [RelayCommand]
    async Task Guardar()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return;
        }

        CurrentInventario.PrecioInicial = Precioinicial;
        WeakReferenceMessenger.Default.Send(CurrentInventario);

        await Cancelar();
    }
}
