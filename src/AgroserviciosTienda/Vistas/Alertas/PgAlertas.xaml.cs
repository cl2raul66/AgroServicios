using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgAlertas : ContentPage
{
	public PgAlertas(PgAlertasVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}