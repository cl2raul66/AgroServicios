using AgroserviciosTienda.Vistas;

namespace AgroserviciosTienda;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(PgAgregarEntrada), typeof(PgAgregarEntrada));
        Routing.RegisterRoute(nameof(PgAgregarProductosEntrada), typeof(PgAgregarProductosEntrada));
        Routing.RegisterRoute(nameof(PgAgregarProveedor), typeof(PgAgregarProveedor));
    }
}
