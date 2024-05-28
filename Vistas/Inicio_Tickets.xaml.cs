using ProyectoTFG.Modelos;
using System.Collections.ObjectModel;


namespace ProyectoTFG.Vistas;

public partial class Inicio_Tickets : ContentPage
{
    public ObservableCollection<User> Users { get; set; }
    public Inicio_Tickets()
	{
		InitializeComponent();

        Users = new ObservableCollection<User>
        {
            new User { Nombre = "User1", Email = "Descripcióooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooon 1", Rol = "imagen1.png" },
            new User { Nombre = "User2", Email = "Descripción 2", Rol = "imagen2.png" },
            new User { Nombre = "User3", Email = "Descripción 3", Rol = "imagen3.png" },
        };

        this.BindingContext = this;

        collection.SelectionChanged += Collection_SelectionChanged;

    }

    private void Collection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        User user = e.CurrentSelection.FirstOrDefault() as User;
        if (user == null)
        {
            return;
        }

        //Navigation.PushAsync(new Inicio_Tickets_Detalle());
        collection.SelectedItem = null;
    }

    private async void OnEnviarCorreoClicked(object sender, EventArgs e)
    {
        await EnviarCorreoAsync("direccion@correo.com", "Asunto del Correo", "Cuerpo del correo");
    }

    private async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo)
    {
        

    }


}