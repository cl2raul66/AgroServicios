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
				fonts.AddFont("MaterialDesignIcons.ttf", "MaterialDesignIcons");
			});
		builder.Services.AddTransient<PgInicioVistaModelo>();
		builder.Services.AddTransient<PgEntradaVistaModelo>();
		builder.Services.AddTransient<PgAgregarEntradaVistaModelo>();
		builder.Services.AddTransient<PgVentaVistaModelo>();
		builder.Services.AddTransient<PgAgregarVentaVistaModelo>();
		builder.Services.AddTransient<PgProductosAddEditVistaModelo>();
		builder.Services.AddTransient<PgProveedorAddEditVistaModelo>();

		builder.Services.AddTransient<PgInicio>();
		builder.Services.AddTransient<PgEntrada>();
		builder.Services.AddTransient<PgAgregarEntrada>();
		builder.Services.AddTransient<PgVenta>();
		builder.Services.AddTransient<PgAgregarVenta>();
		builder.Services.AddTransient<PgProductosAddEdit>();
		builder.Services.AddTransient<PgProveedorAddEdit>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
