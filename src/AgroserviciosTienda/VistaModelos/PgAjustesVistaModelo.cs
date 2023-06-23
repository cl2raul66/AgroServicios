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
        IsActive = true;
        proveedoresServ = contactosRepositorio;
    }

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

    [RelayCommand]
    private async Task VerAgregarproveedor()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarProveedor), true);
    }

    //[RelayCommand]
    //private async Task VerAgregarproveedor()
    //{
    //    await Shell.Current.GoToAsync(nameof(PgAgregarProveedor), true);
    //}

    #region Extra
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
