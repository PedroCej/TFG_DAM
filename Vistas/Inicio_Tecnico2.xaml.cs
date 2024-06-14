namespace ProyectoTFG.Vistas;
using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;
using System.Collections.ObjectModel;
public partial class Inicio_Tecnico2 : ContentPage
{
    public ObservableCollection<Ticket> Tickets { get; set; }
    DB db = new DB();
    private bool seleccion;
    private bool isUpdating;
    private bool botonBorrar;
    private bool ascendente = true;
    private bool iconoOscuro;
    private string iconoAscendente;
    private string iconoDescendente;
    private string iconoPrioridad1;
    private string iconoPrioridad2;
    private string iconoPrioridad3;
    private string iconoSeleccion;
    private string iconoDeseleccion;
    public Inicio_Tecnico2()
	{
        InitializeComponent();

        Tickets = new ObservableCollection<Ticket>(db.GetTicketsAsignados(_AppShell_Inicio.userShell.Email));
        Tickets = new ObservableCollection<Ticket>(Tickets.Reverse());

        this.BindingContext = this;
        collection1.SelectionChanged += Collection_SelectionChanged;


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
        iconoOscuro = oscuro();
        if (iconoOscuro)
        {
            iconoAscendente = "ascendente_dark.png";
            iconoDescendente = "descendente_dark.png";
            iconoPrioridad1 = "prioridad1_dark.png";
            iconoPrioridad2 = "prioridad2_dark.png";
            iconoPrioridad3 = "prioridad3_dark.png";
            iconoSeleccion = "seleccion_dark.png";
            iconoDeseleccion = "deseleccion_dark.png";
        }
        else
        {
            iconoAscendente = "ascendente.png";
            iconoDescendente = "descendente.png";
            iconoPrioridad1 = "prioridad1.png";
            iconoPrioridad2 = "prioridad2.png";
            iconoPrioridad3 = "prioridad3.png";
            iconoSeleccion = "seleccion.png";
            iconoDeseleccion = "deseleccion.png";
        }
        btnSeleccion.Source = iconoDeseleccion;

    }
    public async Task UpdateTickets()
    {
        ObservableCollection<Ticket> updatedTickets = new ObservableCollection<Ticket>(db.GetTicketsAsignados(_AppShell_Inicio.userShell.Email));
        //updatedTickets = new ObservableCollection<Ticket>(updatedTickets.Reverse());
        Device.BeginInvokeOnMainThread(() =>
        {
            Tickets.Clear();
            foreach (var ticket in updatedTickets.Reverse())
            {
                Tickets.Add(ticket);
            }
        });
    }

    public async Task UpdateTicketsHilo()
    {
        ObservableCollection<Ticket> updatedTickets = new ObservableCollection<Ticket>(db.GetTicketsAsignados(_AppShell_Inicio.userShell.Email));

        if (updatedTickets.Count == Tickets.Count)
        {
            return;
        }
        Device.BeginInvokeOnMainThread(() =>
        {
            Tickets.Clear();
            foreach (var ticket in updatedTickets.Reverse())
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

        Navigation.PushModalAsync(new Inicio_Tecnico_Ticket(ticket, this, true));
        collection1.SelectedItem = null;
    }


    private void btnSeleccion_Clicked(object sender, EventArgs e)
    {
        if (seleccion == true)
        {
            btnSeleccion.Source = iconoDeseleccion;
            seleccion = false;
        }
        else
        {
            btnSeleccion.Source = iconoSeleccion;
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

        btnMarcarSeguimiento.IsVisible = false;
        CambiarEstado.IsVisible = false;
        btnMarcarTerminado.IsVisible = false;
        CambiarPrioridad.IsVisible = false;
        BorrarDefinitivo.IsVisible = false;
        BorrarTerminados.IsVisible = false;

        foreach (Ticket ticket1 in Tickets)
        {
            if (ticket1.IsChecked == true)
            {
                btnMarcarSeguimiento.IsVisible = true;
                CambiarEstado.IsVisible = true;
                btnMarcarTerminado.IsVisible = true;
                CambiarPrioridad.IsVisible = true;
                BorrarDefinitivo.IsVisible = true;
                BorrarTerminados.IsVisible = true;
            }
        }
    }

    private void Image_Loaded(object sender, EventArgs e)
    {
        Image img = (Image)sender;
        Ticket ticket = (Ticket)img.BindingContext;
        if (ticket.Prioridad == "Alta")
        {
            img.Source = iconoPrioridad3;
        }
        else if (ticket.Prioridad == "Media")
        {
            img.Source = iconoPrioridad2;
        }
        else
        {
            img.Source = iconoPrioridad1;
        }

    }

    private void btnOrdenarTitulo_Clicked(object sender, EventArgs e)
    {
        btnOrdenarPrioridad.Source = null;
        btnOrdenarEstado.Source = null;
        btnOrdenarCategoria.Source = null;
        if (ascendente)
        {
            btnOrdenarTitulo.Source = iconoAscendente;
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
            btnOrdenarTitulo.Source = iconoDescendente;
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
            btnOrdenarPrioridad.Source = iconoAscendente;
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
            btnOrdenarPrioridad.Source = iconoDescendente;
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
            btnOrdenarCategoria.Source = iconoAscendente;
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
            btnOrdenarCategoria.Source = iconoDescendente;
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
            btnOrdenarEstado.Source = iconoAscendente;
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
            btnOrdenarEstado.Source = iconoDescendente;
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

    private async void BorrarDefinitivo_Clicked(object sender, EventArgs e)
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

    private void CambiarPrioridad_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (Ticket ticket in Tickets)
        {
            if (ticket.IsChecked == true)
            {
                if (CambiarPrioridad.SelectedItem.ToString() == "Baja")
                {
                    ticket.Prioridad = "ZBaja";
                }
                ticket.Prioridad = CambiarPrioridad.SelectedItem.ToString();
                db.UpdateTicket(ticket);
            }
        }
        UpdateTickets();

    }

    private async void CambiarEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CambiarEstado.SelectedItem.ToString() == "En proceso")
            await DisplayAlert("Se le asignarán las incidencias seleccionadas", "", "Aceptar");
        foreach (Ticket ticket in Tickets)
        {
            if (ticket.IsChecked == true)
            {
                if (CambiarEstado.SelectedItem.ToString() == "En proceso") ticket.AsignadoA = _AppShell_Inicio.userShell.Email;
                ticket.Estado = CambiarEstado.SelectedItem.ToString();
                db.UpdateTicket(ticket);
            }

        }
        UpdateTickets();
    }

    private async void BorrarTerminados_Clicked(object sender, EventArgs e)
    {
        bool borrar = await DisplayAlert("¿Estás seguro de que quieres borrar definitivamente los tickets terminados?", "No podrás recuperarlos.", "Sí", "No");
        if (borrar)
        {
            foreach (Ticket ticket in Tickets)
            {
                if (ticket.IsChecked == true && ticket.Estado == "Terminado")
                {
                    db.OcultarTicket(ticket);
                }
            }
            UpdateTickets();
        }
    }

    private bool oscuro()
    {
        /*foreach (var resourceDictionary in Application.Current.Resources.MergedDictionaries)
        {
            if (resourceDictionary.ContainsKey("TemaClaro"))
            {
                return false;
            }
            if (resourceDictionary.ContainsKey("TemaDefault"))
            {
                return false;
            }
        }
        return true;*/
        try
        {
            if (_AppShell_Inicio.userShell.Opciones.Tema == "oscuro") { return false; }
            else { return true; }
        }
        catch (Exception e)
        {
            return true;
        }
    }
}