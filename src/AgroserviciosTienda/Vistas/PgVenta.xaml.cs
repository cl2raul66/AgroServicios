using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgVenta : ContentPage
{
	public PgVenta(PgVentaVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}