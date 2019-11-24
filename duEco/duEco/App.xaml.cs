using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace duEco
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new Index();
            MainPage = new NavigationPage(new Index());
            //verificar que tablas están creadas
            Servicio.CoreServicio.ValidarTablasCreadas();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
