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
        Routing.RegisterRoute(nameof(PgProveedoresDetalles), typeof(PgProveedoresDetalles));
        Routing.RegisterRoute(nameof(PgMedidasDetalles), typeof(PgMedidasDetalles));
        Routing.RegisterRoute(nameof(PgAgregarCliente), typeof(PgAgregarCliente));
        Routing.RegisterRoute(nameof(PgClientesDetalles), typeof(PgClientesDetalles));
        Routing.RegisterRoute(nameof(PgEstablecerPrecioInicialProducto), typeof(PgEstablecerPrecioInicialProducto));
        Routing.RegisterRoute(nameof(PgAgregarVenta), typeof(PgAgregarVenta));
    }
}
