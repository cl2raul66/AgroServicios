namespace AgroserviciosTienda.Vistas;

public partial class PgProveedorAddEdit : ContentPage
{
	public PgProveedorAddEdit(PgProveedorAddEditvistamodelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}