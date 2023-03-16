using AgroserviciosTienda.Vistas.Entradas;

namespace AgroserviciosTienda;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(PgEntDetalles), typeof(PgEntDetalles));
        Routing.RegisterRoute($"{nameof(PgEntDetalles)}/{nameof(PgEntAddEdit)}", typeof(PgEntAddEdit));
        Routing.RegisterRoute(nameof(PgVenDetalles), typeof(PgVenDetalles));
        Routing.RegisterRoute($"{nameof(PgVenDetalles)}/{nameof(PgVenAddEdit)}", typeof(PgVenAddEdit));
    }
}
