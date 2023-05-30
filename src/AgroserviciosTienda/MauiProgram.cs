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

        builder.Services.AddTransient<PgInicioVistaModelo>();
        builder.Services.AddTransient<PgInventarioVistaModelo>();
        builder.Services.AddTransient<PgAgregarEntradaVistaModelo>();

        builder.Services.AddTransient<PgInicio>();
        builder.Services.AddTransient<PgInventario>();
        builder.Services.AddTransient<PgAgregarEntrada>();
        builder.Services.AddTransient<PgVentas>();
        builder.Services.AddTransient<PgAjustes>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
