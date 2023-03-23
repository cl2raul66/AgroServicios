using AgroserviciosTienda.VistaModelos.Ventas;

namespace AgroserviciosTienda.Vistas.Ventas;

public partial class PgVenAdd : ContentPage
{
	public PgVenAdd(PgVenAddVistaModelo vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}