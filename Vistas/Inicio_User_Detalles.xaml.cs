using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;
using System.Collections.ObjectModel;

namespace ProyectoTFG.Vistas;

public partial class Inicio_User_Detalles : ContentPage
{
	public User user;
    Inicio_Admin pagina;
    public Inicio_User_Detalles(User user, Inicio_Admin pagina)
	{
		InitializeComponent();

        this.pagina = pagina;
        this.user = user;
        this.BindingContext = this.user;
    }

    private async void btnVoler_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void entryRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (entryRol.SelectedIndex == 0)
        {
            user.Rol = "user";
        }
        else if (entryRol.SelectedIndex == 1)
        {
            user.Rol = "tecnico";
        }
        else
        {
            user.Rol = "admin";
        }
        DB db = new DB();
        db.UpdateUser(user);
        await pagina.UpdateUsers();
        await pagina.UpdateTecnicos();
        await pagina.UpdateAdmins();
        Navigation.PopModalAsync();

    }
}