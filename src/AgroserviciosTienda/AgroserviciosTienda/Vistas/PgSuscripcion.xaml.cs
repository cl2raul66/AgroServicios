using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgSuscripcion : ContentPage
{
	public PgSuscripcion(PgSuscripcionVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}