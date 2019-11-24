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
	public partial class VerAlerta : ContentPage
	{
        int ID_Alerta;
        AlertaModel objAlerta = new AlertaModel();

        public VerAlerta(int alertaID)
		{
			InitializeComponent ();
            DeshabilitarControles();

            ID_Alerta = alertaID;

            MostrarAlertaSeleccionada(ID_Alerta);
		}

        private void MostrarAlertaSeleccionada(int alertaID)
        {
            objAlerta = AlertaServicio.BuscarPorId(alertaID);
            if(objAlerta != null)
            {
                CargarTipoAlerta(objAlerta.tipoAlertaId);
                CargarHuerta(objAlerta.HuertaID);

                TimeSpan tspHoraAlerta = new TimeSpan(objAlerta.FechaHora.Hour, objAlerta.FechaHora.Minute, 00);                

                dtpFechaAlerta.Date = objAlerta.FechaHora;
                tmpHora.Time = tspHoraAlerta;
                txtDescripcion.Text = objAlerta.Descripcion;    
                chkAvisa.IsToggled = objAlerta.avisa == "S" ? true : false;                
            }
        }

        private void CargarTipoAlerta(String tipoAlertaID)
        {
            var lstTiposAlerta = TipoAlertaServicio.todosLosTiposAlerta();
            cmbTipoAlerta.ItemsSource = lstTiposAlerta;

            foreach (TipoAlertaModel item in lstTiposAlerta)
            {
                if (item.Id == tipoAlertaID)
                    cmbTipoAlerta.SelectedItem = item;
            }             
        }

        private void CargarHuerta(string huertaID)
        {
            var objHuerta = HuertaServicio.BuscarPorId(huertaID);
            if (objHuerta != null)            
                txtHuerta.Text = objHuerta.Nombre;                            
        }


        private void DeshabilitarControles()
        {
            dtpFechaAlerta.IsEnabled = false;
            tmpHora.IsEnabled = false;
            txtDescripcion.IsEnabled = false;
            chkAvisa.IsEnabled = false;
            cmbTipoAlerta.IsEnabled = false;
            btnEditarAlerta.IsVisible = false;
        }

        private void HabilitarControles()
        {
            dtpFechaAlerta.IsEnabled = true;
            tmpHora.IsEnabled = true;
            txtDescripcion.IsEnabled = true;
            chkAvisa.IsEnabled = true;
            cmbTipoAlerta.IsEnabled = true;
            btnEditarAlerta.IsVisible = true;
            btnHabilitaEdicion.IsVisible = false;
        }

        private void BtnHabilitaEdicion_Clicked(object sender, EventArgs e)
        {
            HabilitarControles();            
        }

        private async void BtnEditarAlerta_Clicked(object sender, EventArgs e)
        {
            var userLog = App.Current.Properties["user"].ToString();

            if (userLog != null)
            {
                //PRIMERO ELIMINO LA ALERTA ANTERIOR
                var okEliminado = AlertaServicio.EliminarAlerta(objAlerta.Id, objAlerta.avisa);
                if (okEliminado)
                {
                    DateTime dtmFinal = new DateTime(dtpFechaAlerta.Date.Year,
                                                    dtpFechaAlerta.Date.Month,
                                                    dtpFechaAlerta.Date.Day,
                                                    tmpHora.Time.Hours,
                                                    tmpHora.Time.Minutes,
                                                    00);

                    TipoAlertaModel objTipoAlertaSelec = new TipoAlertaModel();
                    objTipoAlertaSelec = (TipoAlertaModel)cmbTipoAlerta.SelectedItem;
                    String strIdTipoAlertaSelec = objTipoAlertaSelec.Id;

                    var ok = AlertaServicio.CrearAlerta(userLog, objAlerta.HuertaID, txtDescripcion.Text, dtmFinal, chkAvisa.IsToggled, strIdTipoAlertaSelec);
                    if (ok)
                    {
                        await DisplayAlert("Registro correcto", "La alerta se ha actualizado correctamente", "Ok");
                        await Navigation.PushAsync(new Calendario());
                    }
                    else
                    {
                        await DisplayAlert("Registro incorrecto", "No se ha podido actualizar la alerta", "Cancelar");
                    }
                }
                else
                {
                    await DisplayAlert("Registro incorrecto", "No se ha podido modificar la alerta original", "Cancelar");
                }
            }
        }

        private async void BtnEliminarAlerta_Clicked(object sender, EventArgs e)
        {
            var ok = AlertaServicio.EliminarAlerta(ID_Alerta, objAlerta.avisa);
            if (ok)
            {
                await DisplayAlert("Registro correcto", "La alerta se ha eliminado correctamente", "Ok");
                await Navigation.PushAsync(new Calendario());
            }
            else
            {
                await DisplayAlert("Registro incorrecto", "No se ha podido eliminar la alerta", "Cancelar");
            }
        }
    }
}