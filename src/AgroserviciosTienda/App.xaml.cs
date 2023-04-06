using System.Globalization;

namespace AgroserviciosTienda;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        var culture = new CultureInfo("es-ES");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        MainPage = new AppShell();
	}
}
