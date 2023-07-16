using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgAgregarCliente : ContentPage
{
	public PgAgregarCliente(PgAgregarClienteVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}