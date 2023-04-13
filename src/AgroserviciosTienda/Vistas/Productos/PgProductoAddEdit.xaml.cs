using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgProductoAddEdit : ContentPage
{
    public PgProductoAddEdit(PgProductoAddEditVistaModelo vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}