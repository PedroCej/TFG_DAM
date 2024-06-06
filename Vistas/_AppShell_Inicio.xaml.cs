using ProyectoTFG.Datos;
using ProyectoTFG.Modelos;
using System.Diagnostics;

namespace ProyectoTFG.Vistas
{

    public partial class _AppShell_Inicio : Shell
    {
        public static User userShell;
        private DB db = new DB();

        public _AppShell_Inicio()
        {
            InitializeComponent();
            pagAdmin.IsVisible = false;
            pagTecnico.IsVisible = false;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }

        public _AppShell_Inicio(string user)
        {
            InitializeComponent();
            userShell = db.GetUser(user);

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


            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                FlyoutBehavior = FlyoutBehavior.Flyout;
            }

        }

    }
}