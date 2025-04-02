namespace Contador_de_Horas
{
    using Contador_de_Horas.Services;
    using System.Globalization;

    public partial class App : Application
    {
        public static RegistroDatabase Database { get; private set; }

        public App()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-AR");

            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "registro.db3");
            Database = new RegistroDatabase(dbPath);


            // Solo para limpiar la base de datos durante el desarrollo
            //App.Database.EliminarTodo().Wait();

            MainPage = new AppShell();
        }

    }
}
