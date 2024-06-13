using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;
using ProyectoTFG.Resources.Temas;

namespace ProyectoTFG.Vistas;

public partial class Inicio_Ajustes : ContentPage
{
    private User user;
    private string tema;
    private string tamLetra;
    public Inicio_Ajustes()
	{
		InitializeComponent();
        user = _AppShell_Inicio.userShell;
        entryNombre.Placeholder = user.Nombre;
        entryApellidos.Placeholder = user.Apellidos;
        lblEmail.Text = user.Email;
        lblRol.Text = user.Rol;
        if(user.FotoPerfil != null)
        {
            btnFoto.Source = ImageSource.FromStream(() => new MemoryStream(user.FotoPerfil));
        }
        if(user.Opciones != null)
        {
            if (user.Opciones.Tema == "oscuro")
            {
                oscuroTema.IsChecked = true;
            }else if(user.Opciones.Tema == "claro")
            {
                claroTema.IsChecked = true;
            }
            else
            {
                defaultTema.IsChecked = true;
            }
            miSlider.Value = user.Opciones.TamLetra == "default" ? 1 : Double.Parse(user.Opciones.TamLetra);
        }
    }

    private async void btnFoto_Clicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        {
            Title = "Por favor selecciona una foto"
        });

        if (result != null)
        {
            var stream = await result.OpenReadAsync();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            user.FotoPerfil = memoryStream.ToArray();

            btnFoto.Source = ImageSource.FromStream(() => new MemoryStream(user.FotoPerfil));
        }
    }

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton miRadioButton = sender as RadioButton;
        if (miRadioButton.IsChecked)
        {
            ICollection<ResourceDictionary> miListaDiccionarios;
            miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
            if (miRadioButton.Value.ToString() == "0")
            {
                miListaDiccionarios.Clear();
                miListaDiccionarios.Add(new TemaDefault());
                tema = "default";

            }
            if (miRadioButton.Value.ToString() == "1")
            {
                miListaDiccionarios.Clear();
                miListaDiccionarios.Add(new TemaClaro());
                //SemanticScreenReader.Default.Announce("tema claro");
                tema = "claro";
            }
            if (miRadioButton.Value.ToString() == "2")
            {
                miListaDiccionarios.Clear();
                miListaDiccionarios.Add(new TemaOscuro());
                //SemanticScreenReader.Default.Announce("tema oscuro");
                tema = "oscuro";
            }
        }
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        Slider slider = sender as Slider;
        App.Current.Resources["tamanoLetra"] = 15 * slider.Value;
        App.Current.Resources["tamanoLetraTitulo"] = 40 * slider.Value;
        App.Current.Resources["tamanoLetraTitulo2"] = 20 * slider.Value;
        SemanticScreenReader.Default.Announce("cambiando tamaño");
        tamLetra = slider.Value.ToString();

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        DB db = new DB();
        try
        {
            if (entryContrasena.Text == entryContrasena2.Text && entryContrasena.Text!="" && entryContrasena!=null)
            {
                user.UpdatePassword(entryContrasena.Text);
            }
        }catch (Exception ex)
        {
        }

        
        user.Nombre = entryNombre.Text;
        user.Apellidos = entryApellidos.Text;
        Opciones opciones = new Opciones(tema,"default",tamLetra);
        user.Opciones = opciones;

        db.UpdateUser(user);

        DisplayAlert("Cambios guardados", "Se han guardado los cambios correctamente", "Aceptar");

    }
}