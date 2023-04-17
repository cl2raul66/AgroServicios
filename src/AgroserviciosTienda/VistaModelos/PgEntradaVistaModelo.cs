using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgEntradaVistaModelo : ObservableRecipient
{
    readonly IEntradasRepositorio entradasServ;

    public PgEntradaVistaModelo(IEntradasRepositorio entradasRepo)
    {
        IsActive = true;
        entradasServ = entradasRepo;

        Entradas = entradasServ.AnyEntrada ? new(entradasRepo.GetAll) : new();
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        WeakReferenceMessenger.Default.Register<PgEntradaVistaModelo, Entrada>(this, (r, m) =>
        {
            if (m is not null)
            {
                entradasServ.Insert(m);
                Entradas.Insert(0, m);
            }
        });
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
    }

    [ObservableProperty]
    ObservableCollection<Entrada> entradas;

    [RelayCommand]
    private async Task Agregar()
    {
        await Shell.Current.GoToAsync($"{nameof(PgAgregarEntrada)}");
    }
}