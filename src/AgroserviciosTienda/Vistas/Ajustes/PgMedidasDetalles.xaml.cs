using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgMedidasDetalles : ContentPage
{
	public PgMedidasDetalles(PgMedidasDetallesVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}