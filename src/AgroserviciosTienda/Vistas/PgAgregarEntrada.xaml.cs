using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgAgregarEntrada : ContentPage
{
	public PgAgregarEntrada(PgAgregarEntradaVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}