using ProyectoTFG.Modelos;
using System.Runtime.CompilerServices;

namespace ProyectoTFG.Vistas;

public partial class Inicio_Tickets_Detalle : ContentPage
{private Ticket ticket;
	public Inicio_Tickets_Detalle(Ticket ticket)
	{
        
		InitializeComponent();

        this.ticket = ticket;
        Titulo.Text = ticket.Titulo;
        Descripcion.Text = ticket.Descripcion;
        if(ticket.Prioridad == "Baja")
        {
            Prioridad.Value = 1;
        }
        else if (ticket.Prioridad == "Media")
        {
            Prioridad.Value = 2;
        }
        else
        {
            Prioridad.Value = 3;
        }

        try
        {
            btnImagen.Source = ImageSource.FromStream(() => new MemoryStream(ticket.Imagen));
        }catch (Exception e)
        {
            btnImagen.Source = "images.png";
        }

        FechaCreacion.Text = ticket.FechaInicio.ToShortDateString();
        FechaModificacion.Text = ticket.FechaUltimaModificacion.ToShortDateString();
        Usuario.Text = ticket.Usuario;
        Tecnico.Text = ticket.AsignadoA;

    }
    private void btnHabilitar_Clicked(object sender, EventArgs e)
    {
        if (!Titulo.IsEnabled)
        {
            btnHabilitar.Text = "Deshabilitar edición";
            Titulo.IsEnabled = true;
            Descripcion.IsEnabled = true;
            Prioridad.IsEnabled = true;
            btnImagen.IsEnabled = true;
            btnAplicar.IsEnabled = false;
            Comentarios.IsEnabled = true;
            Categoria.IsEnabled = true;
        }
        else { 
            btnHabilitar.Text = "Habilitar edición";
            Titulo.IsEnabled = false;
            Descripcion.IsEnabled = false;
            Prioridad.IsEnabled = false;
            btnImagen.IsEnabled = false;
            btnAplicar.IsEnabled = true;
            Comentarios.IsEnabled = false;
            Categoria.IsEnabled = false;
        }
        

    }
    private void Prioridad_ValueChanged(object sender, ValueChangedEventArgs e)
    {

    }

    private void btnImagen_Clicked(object sender, EventArgs e)
    {

    }

    private void btnEnviar_Clicked(object sender, EventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    

    private void btnAplicar_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnVoler_Clicked(object sender, EventArgs e)
    {
        if (Titulo.IsEnabled)
        {
            bool salir = await DisplayAlert("¿Estás seguro de que quieres volver", "Se perderán los datos modificados en la incidencia.", "Sí", "No");
            if (salir)
            {
                Navigation.PopModalAsync();
            }
        }
        else
        {
            Navigation.PopModalAsync();
        }
        
    }
}