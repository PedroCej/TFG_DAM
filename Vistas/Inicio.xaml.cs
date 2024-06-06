namespace ProyectoTFG.Vistas;

public partial class Inicio : ContentPage
{
	public Inicio()
	{
		InitializeComponent();
	}

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		Navigation.PushModalAsync(new Inicio_CrearTicket());
    }
}