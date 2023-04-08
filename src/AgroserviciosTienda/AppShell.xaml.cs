using AgroserviciosTienda.Vistas;
using AgroserviciosTienda.Vistas.Ajustes;

namespace AgroserviciosTienda;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(PgAgregarEntrada), typeof(PgAgregarEntrada));
        Routing.RegisterRoute(nameof(PgAgregarVenta), typeof(PgAgregarVenta));
        Routing.RegisterRoute(nameof(PgProductoAddEdit), typeof(PgProductoAddEdit));
        Routing.RegisterRoute(nameof(PgContactoAddEdit), typeof(PgContactoAddEdit));
        Routing.RegisterRoute(nameof(PgSetUnidadesMedida), typeof(PgSetUnidadesMedida));
    }
}
