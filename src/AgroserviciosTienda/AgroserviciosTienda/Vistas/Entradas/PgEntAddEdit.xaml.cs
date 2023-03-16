using AgroserviciosTienda.VistaModelos.Entradas;

namespace AgroserviciosTienda.Vistas.Entradas;

public partial class PgEntAddEdit : ContentPage
{
	public PgEntAddEdit(PgEntAddEditVistaModelo vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}