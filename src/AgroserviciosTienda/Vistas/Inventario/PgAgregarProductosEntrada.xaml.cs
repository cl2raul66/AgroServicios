using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgAgregarProductosEntrada : ContentPage
{
	public PgAgregarProductosEntrada(PgAgregarEntradaVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}