using AgroserviciosTienda.Utiles;
using AgroserviciosTienda.VistaModelos;

namespace AgroserviciosTienda.Vistas;

public partial class PgAgregarVenta : ContentPage
{
	public PgAgregarVenta(PgAgregarVentaVistaModelo vm)
	{
		InitializeComponent();

		BindingContext = vm;
    }
}