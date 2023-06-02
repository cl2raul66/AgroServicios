using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgAgregarProveedor : ContentPage
{
	public PgAgregarProveedor(PgAgregarProveedorVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}