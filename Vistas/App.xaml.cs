namespace ProyectoTFG.Vistas
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new _AppShell_Login();
        }
    }
}