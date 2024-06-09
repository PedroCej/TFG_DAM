using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;
using System.Collections.ObjectModel;


namespace ProyectoTFG.Vistas;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Inicio_Tickets : ContentPage
{
    public ObservableCollection<Ticket> Tickets { get; set; }
    DB db = new DB();
    private bool seleccion;
    private bool isUpdating;
    private bool botonBorrar;
    private bool ascendente=true;

    public Inicio_Tickets()
    {
        InitializeComponent();

        Tickets = new ObservableCollection<Ticket>(db.GetTickets(_AppShell_Inicio.userShell.Email, false));
        Tickets = new ObservableCollection<Ticket>(Tickets.Reverse());

        this.BindingContext = this;

        Thread hilo = new Thread(async () =>
        {
            while (true)
            {
                if (!isUpdating)
                {
                    isUpdating = true;
                    await UpdateTicketsHilo();
                    isUpdating = false;
                }
                await Task.Delay(10000);
            }
        });
        hilo.Start();

    }
    public async Task UpdateTickets()
    {
        ObservableCollection<Ticket> updatedTickets = new ObservableCollection<Ticket>(db.GetTickets(_AppShell_Inicio.userShell.Email, false));
        Device.BeginInvokeOnMainThread(() =>
        {
            Tickets.Clear();
            foreach (var ticket in updatedTickets)
            {
                Tickets.Add(ticket);
            }
        });
    }

    public async Task UpdateTicketsHilo()
    {
        ObservableCollection<Ticket> updatedTickets = new ObservableCollection<Ticket>(db.GetTickets(_AppShell_Inicio.userShell.Email, false));

        if (updatedTickets.Count == Tickets.Count)
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

        Navigation.PushModalAsync(new Inicio_Tickets_Detalle(ticket, this));
        
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

    private void btnOrdenarTitulo_Clicked(object sender, EventArgs e)
    {
        btnOrdenarPrioridad.Source = null;
        btnOrdenarEstado.Source = null;
        btnOrdenarCategoria.Source = null;
        if (ascendente)
        {
            btnOrdenarTitulo.Source = "ascendente.png";
            var sortedTickets = new ObservableCollection<Ticket>(Tickets.OrderBy(ticket => ticket.Titulo));
            Tickets.Clear();
            foreach (var ticket in sortedTickets)
            {
                Tickets.Add(ticket);
            }
            ascendente = false;
        }
        else
        {
            btnOrdenarTitulo.Source = "descendente.png";
            var sortedTickets = new ObservableCollection<Ticket>(Tickets.OrderByDescending(ticket => ticket.Titulo));
            Tickets.Clear();
            foreach (var ticket in sortedTickets)
            {
                Tickets.Add(ticket);
            }
            ascendente = true;
        }
    }

    private void btnOrdenarPrioridad_Clicked(object sender, EventArgs e)
    {
        btnOrdenarTitulo.Source = null;
        btnOrdenarEstado.Source = null;
        btnOrdenarCategoria.Source = null;
        if (ascendente)
        {
            btnOrdenarPrioridad.Source = "ascendente.png";
            var sortedTickets = new ObservableCollection<Ticket>(Tickets.OrderBy(ticket => ticket.Prioridad));
            Tickets.Clear();
            foreach (var ticket in sortedTickets)
            {
                Tickets.Add(ticket);
            }

            ascendente = false;
        }
        else
        {
            btnOrdenarPrioridad.Source = "descendente.png";
            var sortedTickets = new ObservableCollection<Ticket>(Tickets.OrderByDescending(ticket => ticket.Prioridad));
            Tickets.Clear();
            foreach (var ticket in sortedTickets)
            {
                Tickets.Add(ticket);
            }
            ascendente = true;
        }
        BindingContext = null;
        BindingContext = this;

    }

    private void btnOrdenarCategoria_Clicked(object sender, EventArgs e)
    {
        btnOrdenarEstado.Source = null;
        btnOrdenarPrioridad.Source = null;
        btnOrdenarTitulo.Source = null;
        if (ascendente)
        {
            btnOrdenarCategoria.Source = "ascendente.png";
            var sortedTickets = new ObservableCollection<Ticket>(Tickets.OrderBy(ticket => ticket.Categoria));
            Tickets.Clear();
            foreach (var ticket in sortedTickets)
            {
                Tickets.Add(ticket);
            }
            ascendente = false;
        }
        else
        {
            btnOrdenarCategoria.Source = "descendente.png";
            var sortedTickets = new ObservableCollection<Ticket>(Tickets.OrderByDescending(ticket => ticket.Categoria));
            Tickets.Clear();
            foreach (var ticket in sortedTickets)
            {
                Tickets.Add(ticket);
            }
            ascendente = true;
        }
        BindingContext = null;
        BindingContext = this;
    }

    private void btnOrdenarEstado_Clicked(object sender, EventArgs e)
    {
        btnOrdenarCategoria.Source = null;
        btnOrdenarPrioridad.Source = null;
        btnOrdenarTitulo.Source = null;
        if (ascendente)
        {
            btnOrdenarEstado.Source = "ascendente.png";
            var sortedTickets = new ObservableCollection<Ticket>(Tickets.OrderBy(ticket => ticket.Estado));
            Tickets.Clear();
            foreach (var ticket in sortedTickets)
            {
                Tickets.Add(ticket);
            }
            ascendente = false;
        }
        else
        {
            btnOrdenarEstado.Source = "descendente.png";
            var sortedTickets = new ObservableCollection<Ticket>(Tickets.OrderByDescending(ticket => ticket.Estado));
            Tickets.Clear();
            foreach (var ticket in sortedTickets)
            {
                Tickets.Add(ticket);
            }
            ascendente = true;
        }
        BindingContext = null;
        BindingContext = this;
    }

    private void Image_BindingContextChanged(object sender, EventArgs e)
    {
        Image_Loaded(sender, e);
    }
}
