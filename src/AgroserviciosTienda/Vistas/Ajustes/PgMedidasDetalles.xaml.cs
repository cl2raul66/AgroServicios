using AgroserviciosTienda.Servicios;
using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas.Ajustes;

public partial class PgMedidasDetalles : ContentPage
{
	public PgMedidasDetalles(PgMedidasDetallesVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}