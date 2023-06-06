using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgAgregarProductosEntrada : ContentPage
{
	public PgAgregarProductosEntrada(PgAgregarProductosEntradaVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}