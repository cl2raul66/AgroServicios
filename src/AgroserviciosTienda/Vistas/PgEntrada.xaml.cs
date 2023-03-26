using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgEntrada : ContentPage
{
	public PgEntrada(PgEntradaVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}