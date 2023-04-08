using AgroserviciosTienda.Vistas.Ajustes;

namespace AgroserviciosTienda.Vistas;

public partial class PgAjustes : ContentPage
{
	public PgAjustes()
	{
		InitializeComponent();
	}

    private async void TextCell_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PgSetUnidadesMedida));
    }
}