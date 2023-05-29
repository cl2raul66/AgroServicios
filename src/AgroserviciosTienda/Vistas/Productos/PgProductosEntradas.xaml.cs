using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgProductosEntradas : ContentPage
{
    public PgProductosEntradas(PgProductosEntradasVistaModelo vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}