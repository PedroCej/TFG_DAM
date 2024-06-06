using ProyectoTFG.Datos;

namespace ProyectoTFG.Vistas;

public partial class Login_Register : ContentPage
{
	public DB db = new DB();
    Login login;
    public Login_Register(Login login)
	{
		InitializeComponent();
        this.login = login;
    }

    private void btnRegister_Clicked(object sender, EventArgs e)
    {
        if(txtPass.Text != txtPass2.Text || txtNombre.Text==null || txtEmail.Text==null || txtNombre.Text == "" || txtEmail.Text == "")
        {
            DisplayAlert("Error", "Hay alg�n campo incorrecto o vacio", "Aceptar");
        }
        else
        {
            Preferences.Set("nombreUsuario", txtNombre.Text);
            login.verPerfil();
            db.MeterUser(txtNombre.Text, txtApellidos.Text, txtEmail.Text, txtApodo.Text, txtPass.Text, "user");
            Navigation.PopAsync();
        }
        
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void txtApodo_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (db.ExisteUser(txtApodo.Text))
        {
            lblErrorApodo.IsVisible = true;
            btnRegister.IsEnabled = false;
        }
        else
        {
            lblErrorApodo.IsVisible = false;
            btnRegister.IsEnabled = true;
        }
    }

    private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (db.ExisteUser(txtEmail.Text))
        {
            lblErrorEmail.IsVisible = true;
            btnRegister.IsEnabled = false;
        }
        else
        {
            lblErrorEmail.IsVisible = false;
            btnRegister.IsEnabled = true;
        }
    }

    private void txtPass_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (txtPass.Text != txtPass2.Text)
        {
            lblErrorPass.IsVisible = true;
            btnRegister.IsEnabled = false;
        }
        else
        {
            lblErrorPass.IsVisible = false;
            btnRegister.IsEnabled = true;
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
            txtPass2.Focus();
        }
    }
}