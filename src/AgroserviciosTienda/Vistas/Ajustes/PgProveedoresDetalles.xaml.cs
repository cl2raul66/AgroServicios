using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgProveedoresDetalles : ContentPage
{
    public PgProveedoresDetalles(PgProveedoresDetallesVistaModelo vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}