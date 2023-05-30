using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgInventario : ContentPage
{
	public PgInventario(PgInventarioVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}