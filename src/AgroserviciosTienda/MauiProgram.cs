using AgroserviciosTienda.Modelos;
using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Servicios;
using AgroserviciosTienda.VistaModelos;
using AgroserviciosTienda.Vistas;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace AgroserviciosTienda;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("icofont.ttf", "icofont");
            });

        builder.Services.AddSingleton<IBaseMedidasServicio, BaseMedidasServicio>();
        builder.Services.AddSingleton<IMedidasServicio, MedidasServicio>();
        builder.Services.AddSingleton<IEntradasRepositorio, EntradasLiteDbServicio>();
        builder.Services.AddSingleton<IInventarioRepositorio, InventarioLiteDbServicio>();
        builder.Services.AddSingleton<IContactosRepositorio<Proveedor>, ProveedoresLiteDbServicio>();
        builder.Services.AddSingleton<IContactosRepositorio<Cliente>, ClientesLiteDbServicio>();
        builder.Services.AddSingleton<IVentasRepositorio, VentasLiteDbServicio>();

        builder.Services.AddTransient<PgInicioVistaModelo>();
        builder.Services.AddTransient<PgInventarioVistaModelo>();
        builder.Services.AddTransient<PgAgregarEntradaVistaModelo>();
        builder.Services.AddTransient<PgAgregarProductosEntradaVistaModelo>();
        builder.Services.AddTransient<PgAgregarProveedorVistaModelo>();
        builder.Services.AddTransient<PgAgregarClienteVistaModelo>();
        builder.Services.AddTransient<PgVentasVistaModelo>();
        builder.Services.AddTransient<PgAjustesVistaModelo>();
        builder.Services.AddTransient<PgProveedoresDetallesVistaModelo>();
        builder.Services.AddTransient<PgMedidasDetallesVistaModelo>();
        builder.Services.AddTransient<PgClientesDetallesVistaModelo>();
        builder.Services.AddTransient<PgEstablecerPrecioInicialProductoVistaModelo>();
        builder.Services.AddTransient<PgAgregarVentaVistaModelo>();

        builder.Services.AddTransient<PgInicio>();
        builder.Services.AddTransient<PgInventario>();
        builder.Services.AddTransient<PgAgregarEntrada>();
        builder.Services.AddTransient<PgAgregarProductosEntrada>();
        builder.Services.AddTransient<PgAgregarProveedor>();
        builder.Services.AddTransient<PgAgregarCliente>();
        builder.Services.AddTransient<PgVentas>();
        builder.Services.AddTransient<PgAjustes>();
        builder.Services.AddTransient<PgProveedoresDetalles>();
        builder.Services.AddTransient<PgMedidasDetalles>();
        builder.Services.AddTransient<PgClientesDetalles>();
        builder.Services.AddTransient<PgEstablecerPrecioInicialProducto>();
        builder.Services.AddTransient<PgAgregarVenta>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}
