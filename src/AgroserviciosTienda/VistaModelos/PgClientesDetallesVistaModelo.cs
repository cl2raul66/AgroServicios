using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgClientesDetallesVistaModelo : ObservableRecipient
{
    public PgClientesDetallesVistaModelo()
    {
        IsActive = true;

        Clientes = null;
    }

    [ObservableProperty]
    ObservableCollection<Cliente> clientes;

    [ObservableProperty]
    Cliente selectedCliente;

    [RelayCommand]
    async Task VerAgregarcliente()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarProveedor), true);
    }

    [RelayCommand]
    async Task Eliminarcliente()
    {
        var result = await Application.Current.MainPage.DisplayAlert("Pregunta", $"¿Deseas eliminar ¨{SelectedCliente.Nombre}?", "Sí", "No");

        if (result)
        {
            if (Clientes.Remove(SelectedCliente))
            {
                //proveedoresServ.Delete(SelectedProvvedor.Nombre);
            }
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
                if (Clientes.Count == 0)
                {
                    Clientes = new();
                }
                Clientes.Insert(0, m);
                //proveedoresServ.Insert(m);
            }
        });
    }
    #endregion
}
