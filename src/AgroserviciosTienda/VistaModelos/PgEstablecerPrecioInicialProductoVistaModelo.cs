using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel.DataAnnotations;

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
    string precioinicial;

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
        if (HasErrors || !decimal.TryParse(Precioinicial, out decimal pi))
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return;
        }

        CurrentInventario.PrecioInicial = pi;
        WeakReferenceMessenger.Default.Send(CurrentInventario);

        await Cancelar();
    }
}
