using ProyectoTFG.Datos;

namespace ProyectoTFG.Vistas;

public partial class Login_Register : ContentPage
{
	public DB db = new DB();
    public Login_Register()
	{
		InitializeComponent();
	}

    private void btnRegister_Clicked(object sender, EventArgs e)
    {
        if(txtPass.Text != txtPass2.Text)
        {
            DisplayAlert("Error", "Las contraseñas no coinciden", "Aceptar");
        }
        else
        {
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
}