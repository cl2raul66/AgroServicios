using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroserviciosTienda.VistaModelos;

[QueryProperty(nameof(CurrentEntrada), "entrada")]
public partial class PgAgregarEntradaVistaModelo : ObservableValidator
{
    public PgAgregarEntradaVistaModelo()
    {
        WeakReferenceMessenger.Default.Register<PgAgregarEntradaVistaModelo, Producto>(this, (r, m) =>
        {
            if (m is not null)
            {
                var idx = productos.IndexOf(productos.FirstOrDefault(x => x.Nombre == m.Nombre));
                if (idx > -1)
                {
                    Productos[idx] = m;
                }
                else
                {
                    Productos.Insert(0, m);
                }
            }
        });
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    EntradaView currentEntrada;

    public string Titulo => currentEntrada is null ? "Nueva - Entrada" : "Modificar - Entrada";

    [ObservableProperty]
    DateTime fecha = DateTime.Now;

    [ObservableProperty]
    string noFactura;

    [ObservableProperty]
    Proveedor selectedProveedor;

    [ObservableProperty]
    ObservableCollection<Proveedor> proveedores;

    [ObservableProperty]
    decimal costoFlete;

    [ObservableProperty]
    decimal costoCarga;

    [ObservableProperty]
    bool visibleError;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentEntrada))
        {
            if (currentEntrada is not null)
            {
                Fecha = currentEntrada.Fecha;
                Productos = new(currentEntrada.Productos);
                NoFactura = currentEntrada.NoFactura;
                SelectedProveedor = currentEntrada.Proveedor;
                CostoFlete = currentEntrada.CostoFlete;
                CostoCarga = currentEntrada.CostoCarga;
            }
        }
    }

    [RelayCommand]
    private async void Guardar()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            VisibleError = true;
            await Task.Delay(5000);
            VisibleError = false;
            return;
        }

        EntradaView newEntrada = string.IsNullOrEmpty(noFactura)
            ? new EntradaView(fecha, productos.ToList())
            : new EntradaView(fecha, productos.ToList(), noFactura, selectedProveedor, costoFlete, costoCarga);

        WeakReferenceMessenger.Default.Send<EntradaView>(newEntrada);
        await Cancelar();
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task AgregarProveedor()
    {
        await Shell.Current.GoToAsync($"{nameof(PgProveedorAddEdit)}");
    }

    #region productos
    [ObservableProperty]
    [Required]
    [MinLength(1)]
    ObservableCollection<Producto> productos = new();

    [ObservableProperty]
    Producto selectedProducto;

    [RelayCommand]
    async Task AgregarProducto()
    {
        await Shell.Current.GoToAsync($"{nameof(PgProductosAddEdit)}");
    }

    [RelayCommand]
    async Task ModificarProducto()
    {
        await Shell.Current.GoToAsync($"{nameof(PgProductosAddEdit)}", new Dictionary<string, object>() { { "producto", selectedProducto } });
    }

    [RelayCommand]
    private void EliminarProducto()
    {
        productos.Remove(selectedProducto);
    }
    #endregion
}
