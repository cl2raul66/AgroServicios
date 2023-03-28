using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgContactoAddEdit : ContentPage
{
	public PgContactoAddEdit(PgContactoAddEditVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}