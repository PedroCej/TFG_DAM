using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;

namespace ProyectoTFG.Vistas;

public partial class Login : ContentPage
{
    string nombreUsuario = Preferences.Get("nombreUsuario", string.Empty);
    public DB db = new DB();

    /// <summary>
    /// Pagina de seleccion perfil
    /// </summary>
	public Login()
    {
        InitializeComponent();
        SizeChanged += OnPageSizeChanged;
        if(nombreUsuario != null && nombreUsuario != string.Empty)
        {
            txtPerfil.Text = nombreUsuario;
            verPerfil();
        }
            
    }

    void OnPageSizeChanged(object sender, EventArgs e)
    {
        miStack1.Orientation = Width > 500 ? StackOrientation.Horizontal : StackOrientation.Vertical;
        miStack2.Orientation = Width > 500 ? StackOrientation.Horizontal : StackOrientation.Vertical;
    }

    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {

        // Guardar datos
        // Preferences.Set("nombreUsuario", txtUser.Text);

        txtUser.Text = "";
        txtPass.Text = "";

    }

    private void ImageButton_Usuario(object sender, EventArgs e)
    {
        txtUser.Text = nombreUsuario;
    }

    private void ImageButton_Invitado(object sender, EventArgs e)
    {

    }

    private void Button_Register(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Login_Register(this));
    }

    private void MenuFlyoutItem_Clicked_1(object sender, EventArgs e)
    {
        if(txtUser.Text == null || txtUser.Text == string.Empty)
        {
            DisplayAlert("Error", "Introduce un nombre de usuario", "Aceptar");
            return;
        }
        Preferences.Set("nombreUsuario", txtUser.Text);
        nombreUsuario = Preferences.Get("nombreUsuario", string.Empty);
        txtPerfil.Text = nombreUsuario;

        verPerfil();
    }

    private void Button_Login(object sender, EventArgs e)
    {
       if(db.LoginUser(txtUser.Text, txtPass.Text) && txtPerfil.Text != null && txtPass.Text != null)
        {
            Application.Current.MainPage = new _AppShell_Inicio(txtUser.Text);
        }
        else
        {
            DisplayAlert("Error", "Usuario o contrase�a incorrectos", "Aceptar");
        }
        
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool salir = await DisplayAlert("Salir", "�Est�s seguro de que quieres salir?", "S�", "No");
        if(salir)
        {
            Application.Current.Quit();
        }
        
    }

    private void MenuFlyoutItem_Clicked_2(object sender, EventArgs e)
    {
        Preferences.Set("nombreUsuario", String.Empty);
        ocultarPerfil();
    }

    public void verPerfil()
    {
        btnUser.IsVisible = true;
        txtPerfil.Text = Preferences.Get("nombreUsuario", string.Empty);
        txtPerfil.IsVisible = true;
    }

    public void ocultarPerfil()
    {
        btnUser.IsVisible = false;
        txtPerfil.IsVisible = false;
    }

    private void txtUser_Completed(object sender, EventArgs e)
    {
        txtPass.Focus();
    }
}