namespace ProyectoTFG.Vistas;

public partial class Inicio_Salir : ContentPage
{
	public Inicio_Salir()
	{
		InitializeComponent();

    }

    private void btnAceptar_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new _AppShell_Login();
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        bool pregunta = await DisplayAlert("�Volver a la pagina principal?", "", "S�", "No");
        if (pregunta)
            Application.Current.MainPage = new _AppShell_Inicio(_AppShell_Inicio.userShell.Email);
    }


}