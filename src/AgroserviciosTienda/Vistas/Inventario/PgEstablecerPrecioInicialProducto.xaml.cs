using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgEstablecerPrecioInicialProducto : ContentPage
{
	public PgEstablecerPrecioInicialProducto(PgEstablecerPrecioInicialProductoVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}