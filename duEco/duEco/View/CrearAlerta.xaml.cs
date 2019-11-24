using duEco.Model;
using duEco.Servicio;
using Plugin.LocalNotifications;
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
	public partial class CrearAlerta : ContentPage
	{
        DateTime dtmFechaRec;

        public CrearAlerta(DateTime dtmFechaCreacion)
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

            //OBTENGO LA LISTA DE TIPOS DE ALERTA PARA BINDEAR CON EL COMBO
            var lstTiposAlerta = TipoAlertaServicio.todosLosTiposAlerta();

            cmbTipoAlerta.ItemsSource = lstTiposAlerta;

            //LA FECHA DE LA ALERTA YA VIENE DE LA PANTALLA ANTERIOR (CALENDARIO)
            dtmFechaRec = dtmFechaCreacion;
            lblFecha.Text = dtmFechaRec.ToShortDateString();
        }      

        private async void BtnCrearAlerta_Clicked(object sender, EventArgs e)
        {
            var userLog = App.Current.Properties["user"].ToString();

            if (userLog != null)
            {
                DateTime dtmFinal = new DateTime(dtmFechaRec.Year,
                                                dtmFechaRec.Month,
                                                dtmFechaRec.Day,
                                                tmpHora.Time.Hours,
                                                tmpHora.Time.Minutes,
                                                00);

                HuertaModel objHuertaSelec = new HuertaModel();
                objHuertaSelec = (HuertaModel)cmbHuertas.SelectedItem;
                String strIdHuertaSelec = objHuertaSelec.Id;

                TipoAlertaModel objTipoAlertaSelec = new TipoAlertaModel();
                objTipoAlertaSelec = (TipoAlertaModel)cmbTipoAlerta.SelectedItem;
                String strIdTipoAlertaSelec = objTipoAlertaSelec.Id;

                var ok = AlertaServicio.CrearAlerta(userLog, strIdHuertaSelec, txtDescripcion.Text, dtmFinal, chkAvisa.IsToggled, strIdTipoAlertaSelec);
                if (ok)
                {
                    await DisplayAlert("Registro correcto", "La alerta se ha grabado correctamente", "Ok");
                    await Navigation.PushAsync(new Calendario());
                }
                else
                {
                    await DisplayAlert("Registro incorrecto", "Revise los datos ingresados", "Cancelar");
                }
            }
        }
    }    
}
