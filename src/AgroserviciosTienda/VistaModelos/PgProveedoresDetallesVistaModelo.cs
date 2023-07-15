using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AgroserviciosTienda.VistaModelos;

public partial class PgProveedoresDetallesVistaModelo : ObservableRecipient
{
    readonly IContactosRepositorio<Proveedor> proveedoresServ;

    public PgProveedoresDetallesVistaModelo(IContactosRepositorio<Proveedor> contactosRepositorio)
    {
        IsActive = true;
        proveedoresServ = contactosRepositorio;

        Proveedores = new(proveedoresServ.GetAll);
    }

    [ObservableProperty]
    ObservableCollection<Proveedor> proveedores;

    [ObservableProperty]
    Proveedor selectedProvvedor;

    [RelayCommand]
    async Task VerAgregarproveedor()
    {
        await Shell.Current.GoToAsync(nameof(PgAgregarProveedor), true);
    }

    [RelayCommand]
    async Task Eliminarproveedor()
    {
        var result = await Application.Current.MainPage.DisplayAlert("Pregunta", $"¿Deseas eliminar ¨{SelectedProvvedor.Nombre}?", "Sí", "No");

        if (result)
        {
            if (Proveedores.Remove(SelectedProvvedor))
            {
                var deleteProvvedor = Proveedores.FirstOrDefault(x => x.Nombre == SelectedProvvedor.Nombre);
                Proveedores.Remove(deleteProvvedor);
                proveedoresServ.Delete(SelectedProvvedor.Nombre);
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
        WeakReferenceMessenger.Default.Register<PgProveedoresDetallesVistaModelo, Proveedor>(this, (r, m) =>
        {
            if (m is not null)
            {
                if (Proveedores.Count == 0)
                {
                    Proveedores = new();
                }
                Proveedores.Insert(0, m);
                proveedoresServ.Insert(m);
            }
        });
    }
    #endregion
}
