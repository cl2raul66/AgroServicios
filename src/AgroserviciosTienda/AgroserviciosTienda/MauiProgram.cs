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
		builder.Services.AddTransient<PgSuscripcionVistaModelo>();
		builder.Services.AddTransient<PgSuscripcion>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
