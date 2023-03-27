using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgProveedorAddEdit : ContentPage
{
	public PgProveedorAddEdit(PgProveedorAddEditVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}