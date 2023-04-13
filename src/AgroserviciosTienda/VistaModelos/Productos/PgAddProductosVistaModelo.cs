using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet.Units;
using UnitsNet;
using System.ComponentModel;
using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Servicios;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(DatosNav), "productodatosNav")]
public partial class PgAddProductosVistaModelo : ObservableValidator
{
    readonly IMedidasServicio medidasServ;

    public PgAddProductosVistaModelo(IMedidasServicio medidasServicio)
    {
        medidasServ = medidasServicio;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(DatosNav))
        {
            CurrentProducto = DatosNav.Item1;
        }

        if (e.PropertyName == nameof(CurrentProducto))
        {
            if (CurrentProducto is not null)
            {
                SelectedProductoNombre = CurrentProducto.Nombre;
                Cantidad = CurrentProducto.Cantidad;
                Precio = CurrentProducto.Precio;
            }
        }
    }

    [ObservableProperty]
    Tuple<Producto, bool> datosNav; //Producto currentProducto, bool esVender

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    Producto currentProducto;

    public string Titulo => $"Producto - {(CurrentProducto is null ? "Agregar" : "Modificar")}";

    [ObservableProperty]
    public List<string> productosNombre;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string selectedProductoNombre;

    [ObservableProperty]
    IQuantity presentacion;

    public IEnumerable<string> TiposMedidas => medidasServ.TiposMedidas;

    [ObservableProperty]
    [Required]
    [Range(0.01, 10000.00)]
    double cantidad;

    [ObservableProperty]
    [Required]
    [Range(1.00, 19999.99)]
    decimal precio;

    [ObservableProperty]
    bool visibleError;

    [RelayCommand]
    private async void Agregar()
    {
        if (await Guardar())
        {
            SelectedProductoNombre = string.Empty;
            Cantidad = 0;
            Precio = 0;
        }
    }

    [RelayCommand]
    private async void AgregarSalir()
    {
        if (await Guardar())
        {
            await Cancelar();
        }
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }

    #region extra
    async Task<bool> Guardar()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return false;
        }

        var newProucto = new Producto(SelectedProductoNombre, 1, (decimal)Precio, Quantity.From(Cantidad, LengthUnit.Centimeter));
        var resul = WeakReferenceMessenger.Default.Send<Producto>(newProucto);
        return resul is not null;
    }
    #endregion
}
