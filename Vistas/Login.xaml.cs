namespace ProyectoTFG.Vistas;
public partial class Login : ContentPage
{
    string nombreUsuario = Preferences.Get("nombreUsuario", string.Empty);
    string contraseña = Preferences.Get("contrasena", string.Empty);
    /// <summary>
    /// Pagina de seleccion perfil
    /// </summary>
	public Login()
    {
        InitializeComponent();
        SizeChanged += OnPageSizeChanged;
        txtPerfil.Text = nombreUsuario;

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
        Navigation.PushAsync(new Login_Register());

    }

    private void MenuFlyoutItem_Clicked_1(object sender, EventArgs e)
    {
        Preferences.Set("nombreUsuario", txtUser.Text);
        nombreUsuario = Preferences.Get("nombreUsuario", string.Empty);
        txtPerfil.Text = nombreUsuario;
    }

    private void Button_Login(object sender, EventArgs e)
    {
        Application.Current.MainPage = new _AppShell_Inicio();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}