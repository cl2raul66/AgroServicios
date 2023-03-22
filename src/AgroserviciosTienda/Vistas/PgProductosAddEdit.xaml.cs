using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgProductosAddEdit : ContentPage
{
    public PgProductosAddEdit(PgProductosAddEditVistaModelo vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}