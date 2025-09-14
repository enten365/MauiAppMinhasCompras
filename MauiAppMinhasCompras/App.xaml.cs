using MauiAppMinhasCompras.Helpers;
using System.Globalization;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var cultura = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = cultura;
            Thread.CurrentThread.CurrentUICulture= cultura;

            MainPage = new NavigationPage(new Views.ListaProduto());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Title = "Minhas Compras";
            return window;
        }

        static SQLiteDatabaseHelper _db;
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if(_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.ApplicationData),
                        "banco_sqlite_compras.db3");

                    _db = new SQLiteDatabaseHelper(path);
                }

                return _db;
            }
        }
        /*
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
        */
    }
}