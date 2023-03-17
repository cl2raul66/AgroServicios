using AgroserviciosTienda.VistaModelos.Ventas;

namespace AgroserviciosTienda.Vistas.Entradas;

public partial class PgVenDetalles : ContentPage
{
	public PgVenDetalles(PgVenDetallesVistaModelo vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}