using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace ProyectoTFG.Vistas;

public partial class Inicio_Admin : ContentPage
{
    public DB db = new DB();
    public ObservableCollection<User> Usuarios { get; set; }
    public ObservableCollection<User> Tecnicos { get; set; }
    public ObservableCollection<User> Admins { get; set; }

    public Inicio_Admin()
	{
		InitializeComponent();

        Usuarios = new ObservableCollection<User>(db.GetOnlyUsers());
        Tecnicos = new ObservableCollection<User>(db.GetOnlyTecnicos());
        Admins = new ObservableCollection<User>(db.GetOnlyAdmins());
        this.BindingContext = this; 

    }

    private void listViewUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        User user= e.SelectedItem as User;
        if (user != null)
        {
            Navigation.PushModalAsync(new Inicio_User_Detalles(user,this));
        }

    }

    private void txtApodo_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (db.ExisteUser(txtApodo.Text))
        {
            lblErrorApodo.IsVisible = true;
        }
        else
        {
            lblErrorApodo.IsVisible = false;
        }
    }

    private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
    {
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(emailPattern);
        lblErrorEmail.IsVisible = false;
        if (regex.IsMatch(txtEmail.Text) && !db.ExisteUser(txtEmail.Text))
        {
            lblErrorEmail.IsVisible = false;
        }
        
        if (db.ExisteUser(txtEmail.Text))
        {
            lblErrorEmail.Text = "Error: el email ya existe";
            lblErrorEmail.IsVisible = true;
        }
        if (!regex.IsMatch(txtEmail.Text) && txtEmail.Text != "")
        {
            lblErrorEmail.Text = "Formato: ejemplo@email.com";
            lblErrorEmail.IsVisible = true;
        } 
        
        
                   
    }



    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        if (!txtNombre.IsVisible)
        {
            txtNombre.Text = "";
            txtApellidos.Text = "";
            txtEmail.Text = "";
            txtApodo.Text = "";
            txtPass.Text = "";
            lblErrorApodo.IsVisible = false;
            lblErrorEmail.IsVisible = false;
            txtNombre.IsVisible = true;
            txtApellidos.IsVisible = true;
            txtEmail.IsVisible = true;
            txtApodo.IsVisible = true;
            txtPass.IsVisible = true;
        }
        else
        {
            
            if(txtNombre.Text != "" && txtEmail.Text != "" && txtPass.Text != "")
            {
                try
                {
                    db.MeterUser(txtNombre.Text, txtApellidos.Text, txtEmail.Text, txtApodo.Text, txtPass.Text, "user");
                    UpdateUsers();
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "OK");
                }
            }
                
            lblErrorApodo.IsVisible = false;
            lblErrorEmail.IsVisible = false;
            txtNombre.IsVisible = false;
            txtApellidos.IsVisible = false;
            txtEmail.IsVisible = false;
            txtApodo.IsVisible = false;
            txtPass.IsVisible = false;
        }
        
    }

    private void EntryCompleted(object sender, EventArgs e)
    {
        var currentEntry = sender as Entry;

        if (currentEntry == txtNombre)
        {
            txtApellidos.Focus();
        }
        else if (currentEntry == txtApellidos)
        {
            txtEmail.Focus();
        }
        else if (currentEntry == txtEmail)
        {
            txtApodo.Focus();
        }
        else if (currentEntry == txtApodo)
        {
            txtPass.Focus();
        }
        else if (currentEntry == txtPass)
        {
            ImageButton_Clicked(sender, e);
        }
    }

    public async Task UpdateUsers()
    {
        ObservableCollection<User> updatedUsers = new ObservableCollection<User>(db.GetOnlyUsers());
        Device.BeginInvokeOnMainThread(() =>
        {
            Usuarios.Clear();
            foreach (var user in updatedUsers)
            {
                Usuarios.Add(user);
            }
        });
    }

    public async Task UpdateTecnicos()
    {
        ObservableCollection<User> updatedTecnicos = new ObservableCollection<User>(db.GetOnlyTecnicos());
        Device.BeginInvokeOnMainThread(() =>
        {
            Tecnicos.Clear();
            foreach (var tecnico in updatedTecnicos)
            {
                Tecnicos.Add(tecnico);
            }
        });
    }

    public async Task UpdateAdmins()
    {
        ObservableCollection<User> updatedAdmins = new ObservableCollection<User>(db.GetOnlyAdmins());
        Device.BeginInvokeOnMainThread(() =>
        {
            Admins.Clear();
            foreach (var admin in updatedAdmins)
            {
                Admins.Add(admin);
            }
        });
    }

    private void listViewTecnicos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        User user = e.SelectedItem as User;
        if (user != null)
        {
            Navigation.PushModalAsync(new Inicio_User_Detalles(user, this));
        }

    }

    private void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        if (!txtNombret.IsVisible)
        {
            txtNombret.Text = "";
            txtApellidost.Text = "";
            txtEmailt.Text = "";
            txtApodot.Text = "";
            txtPasst.Text = "";
            lblErrorApodot.IsVisible = false;
            lblErrorEmailt.IsVisible = false;
            txtNombret.IsVisible = true;
            txtApellidost.IsVisible = true;
            txtEmailt.IsVisible = true;
            txtApodot.IsVisible = true;
            txtPasst.IsVisible = true;
        }
        else
        {

            if (txtNombret.Text != "" && txtEmailt.Text != "" && txtPasst.Text != "")
            {
                try
                {
                    db.MeterUser(txtNombret.Text, txtApellidost.Text, txtEmailt.Text, txtApodot.Text, txtPasst.Text, "tecnico");
                    UpdateTecnicos();
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "OK");
                }
            }

            lblErrorApodot.IsVisible = false;
            lblErrorEmailt.IsVisible = false;
            txtNombret.IsVisible = false;
            txtApellidost.IsVisible = false;
            txtEmailt.IsVisible = false;
            txtApodot.IsVisible = false;
            txtPasst.IsVisible = false;
        }

    }

    private void txtEmailt_TextChanged(object sender, TextChangedEventArgs e)
    {
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(emailPattern);
        lblErrorEmailt.IsVisible = false;
        if (regex.IsMatch(txtEmailt.Text) && !db.ExisteUser(txtEmailt.Text))
        {
            lblErrorEmailt.IsVisible = false;
        }

        if (db.ExisteUser(txtEmailt.Text))
        {
            lblErrorEmailt.Text = "Error: el email ya existe";
            lblErrorEmailt.IsVisible = true;
        }
        if (!regex.IsMatch(txtEmailt.Text) && txtEmailt.Text != "")
        {
            lblErrorEmailt.Text = "Formato: ejemplo@email.com";
        }
    }


    private void txtApodot_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (db.ExisteUser(txtApodot.Text))
        {
            lblErrorApodot.IsVisible = true;
        }
        else
        {
            lblErrorApodot.IsVisible = false;
        }

    }

    public void EntryCompletedt(object sender, EventArgs e)
    {
        var currentEntry = sender as Entry;

        if (currentEntry == txtNombret)
        {
            txtApellidost.Focus();
        }
        else if (currentEntry == txtApellidost)
        {
            txtEmailt.Focus();
        }
        else if (currentEntry == txtEmailt)
        {
            txtApodot.Focus();
        }
        else if (currentEntry == txtApodot)
        {
            txtPasst.Focus();
        }
        else if (currentEntry == txtPasst)
        {
            ImageButton_Clicked_1(sender, e);
        }
    }

    private void listViewAdmins_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        User user = e.SelectedItem as User;
        if (user != null)
        {
            Navigation.PushModalAsync(new Inicio_User_Detalles(user, this));
        }

    }

    private void ImageButton_Clicked_2(object sender, EventArgs e)
    {
        if (!txtNombrea.IsVisible)
        {
            txtNombrea.Text = "";
            txtApellidosa.Text = "";
            txtEmaila.Text = "";
            txtApodoa.Text = "";
            txtPassa.Text = "";
            lblErrorApodoa.IsVisible = false;
            lblErrorEmaila.IsVisible = false;
            txtNombrea.IsVisible = true;
            txtApellidosa.IsVisible = true;
            txtEmaila.IsVisible = true;
            txtApodoa.IsVisible = true;
            txtPassa.IsVisible = true;
        }
        else
        {

            if (txtNombrea.Text != "" && txtEmaila.Text != "" && txtPassa.Text != "")
            {
                try
                {
                    db.MeterUser(txtNombrea.Text, txtApellidosa.Text, txtEmaila.Text, txtApodoa.Text, txtPassa.Text, "admin");
                    UpdateAdmins();
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "OK");
                }
            }

            lblErrorApodoa.IsVisible = false;
            lblErrorEmaila.IsVisible = false;
            txtNombrea.IsVisible = false;
            txtApellidosa.IsVisible = false;
            txtEmaila.IsVisible = false;
            txtApodoa.IsVisible = false;
            txtPassa.IsVisible = false;
        }

    }

    private void txtEmaila_TextChanged(object sender, TextChangedEventArgs e)
    {
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        Regex regex = new Regex(emailPattern);
        lblErrorEmaila.IsVisible = false;
        if (regex.IsMatch(txtEmaila.Text) && !db.ExisteUser(txtEmaila.Text))
        {
            lblErrorEmaila.IsVisible = false;
        }

        if (db.ExisteUser(txtEmaila.Text))
        {
            lblErrorEmaila.Text = "Error: el email ya existe";
            lblErrorEmaila.IsVisible = true;
        }
        if (!regex.IsMatch(txtEmaila.Text) && txtEmaila.Text != "")
        {
            lblErrorEmaila.Text = "Formato: example@email.com";
        }
    }

    private void txtApodoa_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (db.ExisteUser(txtApodoa.Text))
        {
            lblErrorApodoa.IsVisible = true;
        }
        else
        {
            lblErrorApodoa.IsVisible = false;
        }

    }

    public void EntryCompleteda(object sender, EventArgs e)
    {
        var currentEntry = sender as Entry;

        if (currentEntry == txtNombrea)
        {
            txtApellidosa.Focus();
        }
        else if (currentEntry == txtApellidosa)
        {
            txtEmaila.Focus();
        }
        else if (currentEntry == txtEmaila)
        {
            txtApodoa.Focus();
        }
        else if (currentEntry == txtApodoa)
        {
            txtPassa.Focus();
        }
        else if (currentEntry == txtPassa)
        {
            ImageButton_Clicked_2(sender, e);
        }
    }

}