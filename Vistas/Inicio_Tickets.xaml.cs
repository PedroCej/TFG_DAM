using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;
using System.Collections.ObjectModel;
using System.Globalization;


namespace ProyectoTFG.Vistas;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Inicio_Tickets : ContentPage
{
    public ObservableCollection<Ticket> Tickets { get; set; }
    DB db = new DB();
    private bool seleccion;
    private bool isUpdating;
    private bool botonBorrar;

    public Inicio_Tickets()
    {
        InitializeComponent();

        Tickets = new ObservableCollection<Ticket>(db.GetTickets(_AppShell_Inicio.userShell.Email, false));

        this.BindingContext = this;
        collection.SelectionChanged += Collection_SelectionChanged;
        Task.Run(async () =>
        {
            while (true)
            {
                if (!isUpdating)
                {
                    isUpdating = true;
                    await UpdateTickets();
                    isUpdating = false;
                }
                await Task.Delay(10000);
            }
        });

        
    }
    private async Task UpdateTickets()
    {
        ObservableCollection<Ticket> updatedTickets = new ObservableCollection<Ticket>(db.GetTickets(_AppShell_Inicio.userShell.Email, false));

        if(updatedTickets.Count == Tickets.Count)
        {
            return;
        }
        Device.BeginInvokeOnMainThread(() =>
        {
            Tickets.Clear();
            foreach (var ticket in updatedTickets)
            {
                Tickets.Add(ticket);
            }
        });
    }

    private void Collection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Ticket ticket = e.CurrentSelection.FirstOrDefault() as Ticket;
        if (ticket == null)
        {
            return;
        }

        Navigation.PushModalAsync(new Inicio_Tickets_Detalle(ticket));
        collection.SelectedItem = null;
    }


    private void btnSeleccion_Clicked(object sender, EventArgs e)
    {
        if (seleccion == true)
        {
            btnSeleccion.Source = "deseleccion.png";
            seleccion = false;
        }
        else
        {
            btnSeleccion.Source = "seleccion.png";
            seleccion = true;
        }

        foreach (Ticket ticket in Tickets)
        {
            ticket.IsChecked = true;
        }
        BindingContext = null;
        BindingContext = this;
    }

    private void CheckBox_BindingContextChanged(object sender, EventArgs e)
    {
        CheckBox ck = (CheckBox)sender;
        ck.IsChecked = seleccion;
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        Ticket ticket = (Ticket)checkBox.BindingContext;
        ticket.IsChecked = checkBox.IsChecked;

        btnBorrar.IsVisible = false;
        foreach (Ticket ticket1 in Tickets)
        {
            if (ticket1.IsChecked == true)
            {
                btnBorrar.IsVisible = true;
            }
        }
    }

    private void Image_Loaded(object sender, EventArgs e)
    {
        Image img = (Image)sender;
        Ticket ticket = (Ticket)img.BindingContext;
        if (ticket.Prioridad == "Alta")
        {
            img.Source = "prioridad3.png";
        }
        else if (ticket.Prioridad == "Media")
        {
            img.Source = "prioridad2.png";
        }
        else
        {
            img.Source = "prioridad1.png";
        }

    }

    private async void btnBorrar_Clicked(object sender, EventArgs e)
    {

        bool borrar = await DisplayAlert("¿Estás seguro de que quieres borrar los tickets seleccionados?", "No podrás recuperarlos.", "Sí", "No");
        if (borrar)
        {
            foreach (Ticket ticket in Tickets)
            {
                if (ticket.IsChecked == true)
                {
                    db.OcultarTicket(ticket);
                }
            }
            UpdateTickets();
        }
    }
}
