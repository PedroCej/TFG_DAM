
using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;
using ProyectoTFG.Resources.Temas;
using System.Diagnostics;

namespace ProyectoTFG.Vistas
{

    public partial class _AppShell_Inicio : Shell
    {
        public static User userShell;
        public string user;
        private DB db = new DB();

        public _AppShell_Inicio()
        {
            InitializeComponent();
            pagAdmin.IsVisible = false;
            pagTecnico.IsVisible = false;
            pagTickets.IsVisible = false;
            pagConf.IsVisible = false;
            user = "Invitado";
        }

        public _AppShell_Inicio(string user)
        {
            InitializeComponent();
            userShell = db.GetUser(user);
            user = userShell.Nombre;
            Application.Current.Resources["user"] = user;
            if (userShell.FotoPerfil != null)
            {
                Application.Current.Resources["imgPerfil"] = ImageSource.FromStream(() => new MemoryStream(userShell.FotoPerfil));
            }
            else
            {
                Application.Current.Resources["imgPerfil"] = "user.png";
            }

            if (userShell.Rol == "admin")
            {
                pagAdmin.IsVisible = true;
                pagTecnico.IsVisible = true;
            } else if (userShell.Rol == "tecnico")
            {
                pagAdmin.IsVisible = false;
                pagTecnico.IsVisible = true;
            } else
            {
                pagAdmin.IsVisible = false;
                pagTecnico.IsVisible = false;
            }

            if (userShell.Opciones != null)
            {
                ICollection<ResourceDictionary> miListaDiccionarios;
                miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
                if (userShell.Opciones.Tema == "oscuro")
                {
                    miListaDiccionarios.Clear();
                    miListaDiccionarios.Add(new TemaOscuro());
                }
                else if (userShell.Opciones.Tema == "claro")
                {
                    miListaDiccionarios.Clear();
                    miListaDiccionarios.Add(new TemaClaro());
                }
                else
                {
                    miListaDiccionarios.Clear();
                    miListaDiccionarios.Add(new TemaDefault());
                }
                
                App.Current.Resources["tamanoLetra"] = 15 * Double.Parse(userShell.Opciones.TamLetra); ;
                App.Current.Resources["tamanoLetraTitulo"] = 40 * Double.Parse(userShell.Opciones.TamLetra); ;
                App.Current.Resources["tamanoLetraTitulo2"] = 20 * Double.Parse(userShell.Opciones.TamLetra); ;
            }


        }

    }
}