namespace ProyectoTFG.Vistas;

using System.Net.Http.Headers;

public partial class Inicio : ContentPage
{
	public Inicio()
	{
		InitializeComponent();
        SizeChanged += OnPageSizeChanged;


    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		Navigation.PushModalAsync(new Inicio_CrearTicket());
    }

    void OnPageSizeChanged(object sender, EventArgs e)
    {
        web.WidthRequest = Width;
        web.HeightRequest = Height;


    }

}