using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace duEco.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public Home ()
		{
			InitializeComponent();
            
            var isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? (bool)App.Current.Properties["IsLoggedIn"] : false;
            if (isLoggedIn)
            {
                //usLog.Text = App.Current.Properties["user"].ToString();
                btnCatalogo.Clicked += btnCatalogo_Clicked;
                btnHuertas.Clicked += btnHuertas_Clicked;
                btnCalendario.Clicked += btnCalendario_Clicked;
                btnReconocer.Clicked += btnReconocer_Clicked;
                btnGraficos.Clicked += btnGrafico_Clicked;
                //btnSalir.Clicked += btnSalir_Clicked;
            }
            else
            {
                DisplayAlert("Mensaje", "No tiene iniciada la sesion", "Ok");
                Navigation.PushAsync(new Index());
            }
		}

        private void btnReconocer_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new View.ReconocerPlanta());
        }

        private void btnHuertas_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new View.MisHuertas());
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            App.Current.Properties["IsLoggedIn"] = false;
            App.Current.Properties["user"] = null;
            Navigation.PushAsync(new Index());
        }

        private void btnCatalogo_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new View.Catalogo());

        }

        private void btnCalendario_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new View.Calendario());
        }

        private void btnGrafico_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new View.MenuGraficos());
        }
    }
}