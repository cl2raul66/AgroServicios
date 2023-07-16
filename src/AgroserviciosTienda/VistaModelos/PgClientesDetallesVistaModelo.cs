using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgClientesDetallesVistaModelo : ObservableRecipient
{
    readonly IContactosRepositorio<Cliente> clientesServ;

    public PgClientesDetallesVistaModelo(IContactosRepositorio<Cliente> contactosRepositorio)
    {
        IsActive = true;

        clientesServ = contactosRepositorio;
        Cargarclientes();
    }

    [ObservableProperty]
    ObservableCollection<Cliente> clientes;

    [ObservableProperty]
    Cliente selectedCliente;

    [RelayCommand]
    async Task VerAgregarcliente()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarCliente), true);
    }

    [RelayCommand]
    async Task Eliminarcliente()
    {
        var result = await Application.Current.MainPage.DisplayAlert("Pregunta", $"¿Deseas eliminar ¨{SelectedCliente.Nombre}?", "Sí", "No");

        if (result)
        {
            clientesServ.Delete(SelectedCliente.Nombre);
            SelectedCliente = null;
            Cargarclientes();
        }
    }

    [RelayCommand]
    async Task GoToBack()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    #region Extra
    protected override void OnActivated()
    {
        base.OnActivated();
        WeakReferenceMessenger.Default.Register<PgClientesDetallesVistaModelo, Cliente>(this, (r, m) =>
        {
            if (m is not null)
            {
                clientesServ.Insert(m);
                Cargarclientes();
            }
        });
    }

    void Cargarclientes()
    {
        Clientes = clientesServ.AnyContacto ? new(clientesServ.GetAll) : null;
    }
    #endregion
}
