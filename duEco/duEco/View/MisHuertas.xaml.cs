using duEco.Servicio;
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
	public partial class MisHuertas : ContentPage
	{
		public MisHuertas ()
		{
            var isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? (bool)App.Current.Properties["IsLoggedIn"] : false;
            if (isLoggedIn)
            {
                InitializeComponent();

                //usLog.Text = App.Current.Properties["user"].ToString();
                //btnCatalogo.Clicked += btnCatalogo_Clicked;
                //btnSalir.Clicked += btnSalir_Clicked;
                var userLog = App.Current.Properties["user"].ToString();
                
                CargarHuertas(userLog);
                btnAgregar.Clicked += BtnAgregar_Clicked;
            }
            else
            {
                DisplayAlert("Mensaje", "No tiene iniciada la sesion", "Ok");
                Navigation.PushAsync(new Index());
            }
        }

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new View.ABMHuerta("0"));
        }

        private void CargarHuertas(string userId)
        {
            var misHuertas = HuertaServicio.TodasLasHuertas(userId);
            if (misHuertas.Count > 0)
            {
                lstHuertas.ItemsSource = misHuertas;
            }
            
        }

        private void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedHuerta = (Model.HuertaModel)e.SelectedItem;
            var item = selectedHuerta.Id.ToString();
            try
            {
                if (selectedHuerta != null)
                {
                    ((NavigationPage)this.Parent).PushAsync(new VerHuerta(item));
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}