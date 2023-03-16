using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgInicio : ContentPage
{
	public PgInicio(PgInicioVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}