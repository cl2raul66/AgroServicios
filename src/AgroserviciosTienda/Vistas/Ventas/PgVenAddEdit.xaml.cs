using AgroserviciosTienda.VistaModelos.Ventas;

namespace AgroserviciosTienda.Vistas.Entradas;

public partial class PgVenAddEdit : ContentPage
{
	public PgVenAddEdit(PgVenAddEditVistaModelo vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}