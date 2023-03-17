using AgroserviciosTienda.VistaModelos;
using AgroserviciosTienda.VistaModelos.Entradas;
using AgroserviciosTienda.VistaModelos.Ventas;
using AgroserviciosTienda.Vistas;
using AgroserviciosTienda.Vistas.Entradas;
using Microsoft.Extensions.Logging;

namespace AgroserviciosTienda;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddTransient<PgInicioVistaModelo>();
		builder.Services.AddTransient<PgEntDetallesVistaModelo>();
		builder.Services.AddTransient<PgEntAddEditVistaModelo>();
		builder.Services.AddTransient<PgVenDetallesVistaModelo>();
		builder.Services.AddTransient<PgVenAddEditVistaModelo>();

		builder.Services.AddTransient<PgInicio>();
		builder.Services.AddTransient<PgEntDetalles>();
		builder.Services.AddTransient<PgEntAddEdit>();
		builder.Services.AddTransient<PgVenDetalles>();
		builder.Services.AddTransient<PgVenAddEdit>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
