using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas.Ajustes;

public partial class PgProveedoresDetalles : ContentPage
{
    public PgProveedoresDetalles(PgProveedoresDetallesVistaModelo vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}