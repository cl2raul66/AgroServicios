using AgroserviciosTienda.Repositorios;
using AgroserviciosTienda.Servicios;
using AgroserviciosTienda.VistaModelos;
using AgroserviciosTienda.VistaModelos.Ajustes;
using AgroserviciosTienda.Vistas;
using AgroserviciosTienda.Vistas.Ajustes;
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
				fonts.AddFont("MaterialDesignIcons.ttf", "MaterialDesignIcons");
			});
		builder.Services.AddSingleton<IProveedoresRepositorio, ProveedoresLiteDbServicio>();
		builder.Services.AddSingleton<IEntradasRepositorio, EntradasLiteDbServicio>();
		builder.Services.AddSingleton<IMedidasServicio, MedidasServicio>();
		builder.Services.AddSingleton<IVentasRepositorio, VentasLiteDbServicio>();

		builder.Services.AddTransient<PgInicioVistaModelo>();
		builder.Services.AddTransient<PgEntradaVistaModelo>();
		builder.Services.AddTransient<PgAgregarEntradaVistaModelo>();
		builder.Services.AddTransient<PgVentaVistaModelo>();
		builder.Services.AddTransient<PgAgregarVentaVistaModelo>();
		builder.Services.AddTransient<PgProductoAddEditVistaModelo>();
		builder.Services.AddTransient<PgAddProductosVistaModelo>();
		builder.Services.AddTransient<PgContactoAddEditVistaModelo>();
		builder.Services.AddTransient<PgSetUnidadesMedidaVistaModelo>();

		builder.Services.AddTransient<PgInicio>();
		builder.Services.AddTransient<PgEntrada>();
		builder.Services.AddTransient<PgAgregarEntrada>();
		builder.Services.AddTransient<PgVenta>();
		builder.Services.AddTransient<PgAgregarVenta>();
		builder.Services.AddTransient<PgProductoAddEdit>();
		builder.Services.AddTransient<PgAddProductos>();
		builder.Services.AddTransient<PgContactoAddEdit>();
		builder.Services.AddTransient<PgSetUnidadesMedida>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
