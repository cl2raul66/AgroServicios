using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgAjustes : ContentPage
{
	public PgAjustes(PgAjustesVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}