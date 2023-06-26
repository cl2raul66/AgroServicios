using AgroserviciosTienda.Servicios;

namespace AgroserviciosTienda.Vistas.Ajustes;

public partial class PgMedidasDetalles : ContentPage
{
	public PgMedidasDetalles(IMedidasServicio vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}