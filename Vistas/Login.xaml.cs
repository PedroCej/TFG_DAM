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
            if (db.ObtenerFotoPerfil(nombreUsuario) != null)
            {
                btnUser.Source = ImageSource.FromStream(() => new MemoryStream(db.ObtenerFotoPerfil(nombreUsuario)));
            }
            verPerfil();
        }
        else
        {
            btnUser.Source = "user.png";
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
        Application.Current.MainPage = new _AppShell_Inicio();
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

    private async void Button_Login(object sender, EventArgs e)
    {
       if(db.LoginUser(txtUser.Text, txtPass.Text) && txtPerfil.Text != null && txtPass.Text != null)
        {
            try
            {
                if(nombreUsuario != txtUser.Text)
                {
                    bool pregunta = await DisplayAlert("Inicio de sesión", "¿Quieres guardar el usuario?", "Sí", "No");
                    if (pregunta)
                    {
                        Preferences.Set("nombreUsuario", txtUser.Text);
                    }
                }
                
                
               // guardarUser();
                Application.Current.MainPage = new _AppShell_Inicio(txtUser.Text);
            }catch (Exception ex)
            {
                DisplayAlert("Error", "error: "+ex, "Aceptar");
            }
            
        }
        else
        {
            DisplayAlert("Error", "Usuario o contraseña incorrectos", "Aceptar");
        }
        
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool salir = await DisplayAlert("Salir", "¿Estás seguro de que quieres salir?", "Sí", "No");
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

   /* private void guardarUser()
    {
        Thread thread = new Thread(() =>
        {
            if (nombreUsuario != txtUser.Text)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    
                });
            }
        });
        thread.IsBackground = true;
        thread.Start();
    }*/

}