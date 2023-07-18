using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Utiles.Mensajes;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgInventarioVistaModelo : ObservableRecipient
{
    readonly IInventarioRepositorio inventarioServ;

    public PgInventarioVistaModelo(IInventarioRepositorio inventarioRepositorio)
    {
        IsActive = true;
        inventarioServ = inventarioRepositorio;

        if (inventarioServ.AnyInventario)
        {
            GetInventario();
        }
    }

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
            }
        });
    }

    [ObservableProperty]
    ObservableCollection<Inventario> almacen;

    [RelayCommand]
    async Task VerAgregarentrada()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarEntrada), true);
    }

    #region Extra
    void GetInventario()
    {
        Almacen = new(inventarioServ.GetAll);
    }

    DateTime GetLunesOfSemana(DateTime fecha)
    {
        int diasHastaLunes = ((int)fecha.DayOfWeek - 1 + 7) % 7;
        DateTime primerDiaSemana = fecha.Date.AddDays(-diasHastaLunes);
        return primerDiaSemana;
    }
    #endregion
}
