using AgroserviciosTienda.VistaModelos.Entradas;

namespace AgroserviciosTienda.Vistas.Entradas;

public partial class PgEntDetalles : ContentPage
{
	public PgEntDetalles(PgEntDetallesVistaModelo vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}