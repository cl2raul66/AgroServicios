using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgVentas : ContentPage
{
	public PgVentas(PgVentasVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}