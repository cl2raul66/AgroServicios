using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgInventarioVistaModelo : ObservableRecipient
{
    readonly IInventarioRepositorio inventarioServ;
    readonly IEntradasRepositorio entradasServ;

    public PgInventarioVistaModelo(IInventarioRepositorio inventarioRepositorio, IEntradasRepositorio entradasRepositorio)
    {
        IsActive = true;
        inventarioServ = inventarioRepositorio;
        entradasServ = entradasRepositorio;

        if (inventarioServ.AnyInventario)
        {
            GetInventario();
        }

        if (entradasServ.AnyEntrada)
        {
            GetEntradas();
        }

        SelectedAlmacen = true;
    }

    public string Titulo => SelectedAlmacen ? "Inventario" : "Entradas";

    [ObservableProperty]
    ObservableCollection<Inventario> almacen;

    [ObservableProperty]
    Inventario selectedItemAlmacen;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Titulo))]
    bool selectedAlmacen;

    [ObservableProperty]
    ObservableCollection<EntradaView> entradas;

    [ObservableProperty]
    bool selectedEntradas;

    [RelayCommand]
    async Task VerAgregarentrada()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarEntrada), true);
    }

    [RelayCommand]
    async Task VerEstablecerprecioinicialproducto()
    {
        await Shell.Current.GoToAsync(nameof(PgEstablecerPrecioInicialProducto), true, new Dictionary<string, object> { { "inventario", SelectedItemAlmacen } });
    }

    [RelayCommand]
    async Task SelectedButtonState(string selectedButton)
    {
        switch (selectedButton)
        {
            case "Almacen":
                SelectedAlmacen = true;
                SelectedEntradas = false;
                break;
            case "Entradas":
                SelectedAlmacen = false;
                SelectedEntradas = true;
                break;
            default:
                SelectedAlmacen = true;
                SelectedEntradas = false;
                break;
        }
        SelectedItemAlmacen = null;
        await Task.CompletedTask;
    }

    #region Extra
    protected override void OnActivated()
    {
        WeakReferenceMessenger.Default.Register<PgInventarioVistaModelo, string>(this, (r, m) =>
        {
            if (string.IsNullOrEmpty(m))
            {
                return;
            }

            bool seAgrego = bool.Parse(m);
            if (seAgrego)
            {
                GetInventario();
                GetEntradas();
            }
        });

        WeakReferenceMessenger.Default.Register<PgInventarioVistaModelo, Inventario>(this, (r, m) =>
        {
            if (m is not null)
            {
                inventarioServ.Upset(m);
                GetInventario();
            }
        });
    }

    void GetInventario()
    {
        Almacen = new(inventarioServ.GetAll);
    }

    void GetEntradas()
    {
        Entradas = new(entradasServ.GetAll.Select(x => new EntradaView(x)));
    }

    DateTime GetLunesOfSemana(DateTime fecha)
    {
        int diasHastaLunes = ((int)fecha.DayOfWeek - 1 + 7) % 7;
        DateTime primerDiaSemana = fecha.Date.AddDays(-diasHastaLunes);
        return primerDiaSemana;
    }
    #endregion
}
