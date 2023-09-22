using AgroserviciosTienda.Modelos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgAlertasVistaModelo : ObservableRecipient
{
    [ObservableProperty]
    string titulo;

    [ObservableProperty]
    string mensaje;

    [ObservableProperty]
    bool visibleAlerta;

    protected override void OnActivated()
    {
        base.OnActivated();

        WeakReferenceMessenger.Default.Register<PgAlertasVistaModelo, Alerta>(this, (r, m) =>
        {
            r.Titulo = m.TipoAlerta.ToString();
            r.Mensaje = m.Mensaje;
            _ = Cerrar();
        });
    }

    [RelayCommand]
    async Task Cerrar()
    {
        if (VisibleAlerta)
        {
            visibleAlerta = false;
        }
        visibleAlerta = true;
        await Task.Delay(5000);
        visibleAlerta = false;
    }
}
