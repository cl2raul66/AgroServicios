using AgroserviciosTienda.VistaModelos.Ajustes;

namespace AgroserviciosTienda.Vistas.Ajustes;

public partial class PgSetUnidadesMedida : ContentPage
{
	public PgSetUnidadesMedida(PgSetUnidadesMedidaVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}