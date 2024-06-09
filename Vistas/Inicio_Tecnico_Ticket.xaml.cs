using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;

namespace ProyectoTFG.Vistas;

public partial class Inicio_Tecnico_Ticket : ContentPage
{
    private Ticket ticket;
    private Inicio_Tecnico pagTickets;
    DB db = new DB();
    public Inicio_Tecnico_Ticket(Ticket ticket, Inicio_Tecnico pagTickets)
    {

        InitializeComponent();

        this.ticket = ticket;
        this.pagTickets = pagTickets;
        Titulo.Text = ticket.Titulo;
        Descripcion.Text = ticket.Descripcion;
        if (ticket.Prioridad == "ZBaja")
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

        if (ticket.Imagen != null)
        {
            btnImagen.Source = ImageSource.FromStream(() => new MemoryStream(ticket.Imagen));
        }

        Categoria.SelectedItem = ticket.Categoria;
        Estado.SelectedItem = ticket.Estado;

        FechaCreacion.Text = ticket.FechaInicio.ToShortDateString();
        FechaModificacion.Text = ticket.FechaUltimaModificacion.ToShortDateString();
        Usuario.Text = ticket.Usuario;
        Tecnico.Text = ticket.AsignadoA;

    }
    private void btnHabilitar_Clicked(object sender, EventArgs e)
    {
        if (!btnAplicar.IsEnabled)
        {
            btnHabilitar.Text = "Deshabilitar edición";
            Titulo.IsEnabled = true;
            Descripcion.IsEnabled = true;
            Prioridad.IsEnabled = true;
            Estado.IsEnabled = true;
            btnImagen.IsEnabled = true;
            btnAplicar.IsEnabled = true;
            Categoria.IsEnabled = true;
        }
        else
        {
            btnHabilitar.Text = "Habilitar edición";
            Titulo.IsEnabled = false;
            Descripcion.IsEnabled = false;
            Prioridad.IsEnabled = false;
            Estado.IsEnabled = false;
            btnImagen.IsEnabled = false;
            btnAplicar.IsEnabled = false;
            Categoria.IsEnabled = false;
        }


    }
    private void Prioridad_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        var newStep = Math.Round(e.NewValue);
        Prioridad.Value = newStep;
    }

    private void btnImagen_Clicked(object sender, EventArgs e)
    {

    }


    private void Button_Clicked(object sender, EventArgs e)
    {

    }



    private void btnAplicar_Clicked(object sender, EventArgs e)
    {
        ticket.Titulo = Titulo.Text;
        ticket.Descripcion = Descripcion.Text;
        if (Prioridad.Value == 1)
        {
            ticket.Prioridad = "ZBaja";
        }
        else if (Prioridad.Value == 2)
        {
            ticket.Prioridad = "Media";
        }
        else if (Prioridad.Value == 3)
        {
            ticket.Prioridad = "Alta";
        }
        ticket.Categoria = Categoria.SelectedItem.ToString();
        ticket.Estado = Estado.SelectedItem.ToString();
        ticket.FechaUltimaModificacion = DateTime.Now;


        db.UpdateTicket(ticket);
        pagTickets.UpdateTickets();
        btnAplicar.IsEnabled = false;
    }

    private async void btnVoler_Clicked(object sender, EventArgs e)
    {
        if (btnAplicar.IsEnabled)
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
