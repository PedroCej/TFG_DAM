
using ProyectoTFG.Datos;
using System.Diagnostics;

namespace ProyectoTFG.Vistas;

public partial class Inicio_CrearTicket : ContentPage
{
    private Byte[] imagen;
    public Inicio_CrearTicket()
	{
		InitializeComponent();
    }

    private void miSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        var newStep = Math.Round(e.NewValue);

        miSlider.Value = newStep;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool salir = await DisplayAlert("¿Estás seguro de que quieres salir?", "Se perderán todos los datos de la incidencia.", "Sí", "No");
        if (salir)
        {
            Navigation.PopModalAsync();
        }
    }

    private void btnEnviar_Clicked(object sender, EventArgs e)
    {
        DB db = new DB();
        string prioridad = "";
        if (miSlider.Value == 1)
        {
            prioridad = "ZBaja";
        }
        else if (miSlider.Value == 2)
        {
            prioridad = "Media";
        }
        else if (miSlider.Value == 3)
        {
            prioridad = "Alta";
        }
        try
        {
            string email = "Invitado";
            if (_AppShell_Inicio.userShell!=null) 
                email = _AppShell_Inicio.userShell.Email;
            if (Titulo.Text != null && Descripcion.Text != null)
            {
            db.UserMeterTicket(Titulo.Text, Descripcion.Text, miPicker.SelectedItem.ToString(), prioridad, DateTime.Now, email, imagen);
            DisplayAlert("Incidencia creada", "Se ha creado la incidencia correctamente", "Aceptar");
            Navigation.PopModalAsync();
            }
            else
            {
                DisplayAlert("Error", "No se ha podido crear la incidencia. \n\nAsegurate de rellenar correctarmente \ntodos los campos.", "Aceptar");
            }


        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "No se ha podido crear la incidencia. \n\nSi necesitas ayuda contacta \ncon un administrador", "Aceptar");
            Debug.WriteLine(ex.Message);
        }
    }
    async void OnSelectImageButtonClicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        {
            Title = "Por favor selecciona una foto"
        });

        if (result != null)
        {
            var stream = await result.OpenReadAsync();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            imagen = memoryStream.ToArray();
            btnImagen.Source = ImageSource.FromStream(() => new MemoryStream(imagen));
        }
    }
}