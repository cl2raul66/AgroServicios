using AgroserviciosTienda.VistaModelos.Ventas;

namespace AgroserviciosTienda.Vistas.Ventas;

public partial class PgVenDetalles : ContentPage
{
	public PgVenDetalles(PgVenDetallesVistaModelo vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}