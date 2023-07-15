using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgClientesDetalles : ContentPage
{
	public PgClientesDetalles(PgClientesDetallesVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}