using AgroserviciosTienda.Vistas;

namespace AgroserviciosTienda;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(PgAgregarEntrada), typeof(PgAgregarEntrada));
        Routing.RegisterRoute(nameof(PgAgregarVenta), typeof(PgAgregarVenta));
        Routing.RegisterRoute(nameof(PgProductosAddEdit), typeof(PgProductosAddEdit));
        Routing.RegisterRoute(nameof(PgContactoAddEdit), typeof(PgContactoAddEdit));
    }
}
