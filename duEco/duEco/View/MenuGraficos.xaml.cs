using duEco.Model;
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
	public partial class MenuGraficos : ContentPage
	{
        DateTime dtmFechaRec;

        public MenuGraficos()
        {
            InitializeComponent();

            //COMPRUEBO QUE ESTÉ LOGUEADO Y BUSCO LAS HUERTAS DEL USUARIO PARA BINDEAR CON EL COMBO
            bool isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? (bool)App.Current.Properties["IsLoggedIn"] : false;
            if (isLoggedIn)
            {
                String userLog = App.Current.Properties["user"].ToString();

                var misHuertas = HuertaServicio.TodasLasHuertas(userLog);
                if (misHuertas.Count > 0)
                {
                    cmbHuertas.ItemsSource = misHuertas;
                }
            }
            else
            {
                DisplayAlert("Mensaje", "No tiene iniciada la sesion", "Ok");
                Navigation.PushAsync(new Index());
            }
        }              

        private void BtnVerGrafico_Clicked(object sender, EventArgs e)
        {
            HuertaModel objHuertaSelec = new HuertaModel();
            objHuertaSelec = (HuertaModel)cmbHuertas.SelectedItem;

            String strIdHuertaSelec = objHuertaSelec.Id;

            try
            {
                if (strIdHuertaSelec != null)
                {
                    ((NavigationPage)this.Parent).PushAsync(new VerGrafico(strIdHuertaSelec));
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }
    }    
}
