using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgAjustesVistaModelo : ObservableRecipient
{
    readonly IContactosRepositorio<Proveedor> proveedoresServ;

    public PgAjustesVistaModelo(IContactosRepositorio<Proveedor> contactosRepositorio)
    {
        proveedoresServ = contactosRepositorio;
    }

    [RelayCommand]
    async Task VerAgregarproveedor()
    {
        IsActive = true;
        await Shell.Current.GoToAsync(nameof(PgAgregarProveedor), true);
    }
    
    [RelayCommand]
    async Task VerAdministrarroveedores()
    {
        IsActive = false;
        await Shell.Current.GoToAsync(nameof(PgProveedoresDetalles), true);
    }

    [RelayCommand]
    async Task VerAdministrarmedidas()
    {
        await Shell.Current.GoToAsync(nameof(PgMedidasDetalles), true);
    }

    [RelayCommand]
    async Task VerAdministrarclientes()
    {
        await Shell.Current.GoToAsync(nameof(PgClientesDetalles), true);
    }

    #region Extra
    protected override void OnActivated()
    {
        base.OnActivated();

        WeakReferenceMessenger.Default.Register<PgAjustesVistaModelo, Proveedor>(this, (r, m) =>
        {
            if (m is not null)
            {
                proveedoresServ.Insert(m);
                _ = ViewToast($"Se agrego el proveedor: {m.Nombre}");
            }
        });
    }
    async Task ViewToast(string mensaje)
    {
        CancellationTokenSource cancellationTokenSource = new();
        string text = mensaje;
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;
        var toast = Toast.Make(text, duration, fontSize);
        await toast.Show(cancellationTokenSource.Token);
    }
    #endregion
}
