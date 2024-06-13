namespace ProyectoTFG.Vistas;

using System.Net.Http.Headers;

public partial class Inicio : ContentPage
{
	public Inicio()
	{
		InitializeComponent();
        SizeChanged += OnPageSizeChanged;
        animar();

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

      private async void animar()
    {
        while (true)
        {
            await flecha.TranslateTo(-150, 0, 1000, Easing.Linear);
    
            await flecha.TranslateTo(0, 0, 1000, Easing.CubicInOut);
        }
    }


   


}