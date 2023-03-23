using AgroserviciosTienda.Vistas;
using AgroserviciosTienda.Vistas.Entradas;
using AgroserviciosTienda.Vistas.Ventas;

namespace AgroserviciosTienda;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(PgEntDetalles), typeof(PgEntDetalles));
        Routing.RegisterRoute(nameof(PgEntAddEdit), typeof(PgEntAddEdit));
        Routing.RegisterRoute(nameof(PgVenDetalles), typeof(PgVenDetalles));
        Routing.RegisterRoute(nameof(PgVenAdd), typeof(PgVenAdd));
        Routing.RegisterRoute(nameof(PgProductosAddEdit), typeof(PgProductosAddEdit));
    }
}
