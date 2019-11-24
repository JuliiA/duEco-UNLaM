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
using XamForms.Controls;

namespace duEco.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Calendario : ContentPage
	{
        List<AlertaModel> lstMisAlertas = new List<AlertaModel>();

        public Calendario ()
		{            
            var isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? (bool)App.Current.Properties["IsLoggedIn"] : false;
            if (isLoggedIn)
            {
                InitializeComponent();

                calCalendario.SelectedDate = new DateTime(2019,11,20);

                var userLog = App.Current.Properties["user"].ToString();

                CargarAlertas(userLog);

                CalCalendario_DateClicked(calCalendario, new DateTimeEventArgs());
            }
        }

        private void CargarAlertas(string userLog)
        {
            lstMisAlertas = AlertaServicio.TodasLasAlertas(userLog);
            if (lstMisAlertas.Count > 0)
            {
                ColorearDiasConAlertas(lstMisAlertas);
            }
        }

        private void ColorearDiasConAlertas(List<AlertaModel> listaAlertas)
        {
            calCalendario.SpecialDates.Clear();
            foreach (AlertaModel Alerta in listaAlertas)
            {
                SpecialDate oDate = new SpecialDate(Alerta.FechaHora);
                oDate.BackgroundColor = Color.MediumSeaGreen;
                oDate.Selectable = true;
                calCalendario.SpecialDates.Add(oDate);
            }
        }

        private void BtnNuevaAlerta_Clicked(object sender, EventArgs e)
        {
            DateTime dtmFechaSeleccionada = Convert.ToDateTime(calCalendario.SelectedDate);

            Navigation.PushAsync(new View.CrearAlerta(dtmFechaSeleccionada));
        }        

        private void LstAlertas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedAlerta = (Model.AlertaModel)e.SelectedItem;
            int idItem = selectedAlerta.Id;
            try
            {
                if (selectedAlerta != null)
                {
                    ((NavigationPage)this.Parent).PushAsync(new VerAlerta(idItem));
                }
            }
            catch (Exception x)
            {
                throw x;
            }            
        }        

        private void CalCalendario_DateClicked(object sender, DateTimeEventArgs e)
        {
            lstAlertas.ItemsSource = null;
            var userLog = App.Current.Properties["user"].ToString();
            DateTime fechaSeleccionada;

            //if (e.DateTime. == Convert.ToDateTime("00010101 00:00:00"))
            if (e.DateTime == default(DateTime))
                fechaSeleccionada = Convert.ToDateTime(calCalendario.SelectedDate);
            else
                fechaSeleccionada = e.DateTime;


            var lstAlertasPorDia = AlertaServicio.AlertasPorDia(userLog, fechaSeleccionada);
            if (lstAlertasPorDia.Count > 0)
            {
                lstAlertas.ItemsSource = lstAlertasPorDia;
            }
        }
    }
}