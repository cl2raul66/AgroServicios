using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgAddProductos : ContentPage
{
	public PgAddProductos(PgAddProductosVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}