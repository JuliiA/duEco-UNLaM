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
	public partial class CultivoCalendario : ContentPage
	{
        private string _laHuerta;
        private string _laPlanta;

		public CultivoCalendario (string planta, string huerta)
		{
			InitializeComponent ();

            _laPlanta = planta;
            _laHuerta = huerta;

            btnConfirmar.Clicked += btnConfirmar_Clicked;
        }

        private async void btnConfirmar_Clicked(object sender, EventArgs e)
        {
            var fechaSiembraSelec = dpSiembra.Date;
            var idCultivo = CoreServicio.Encrypt.GetMD5(_laPlanta.ToString().Substring(3));
            var addCultivo = CultivoServicio.CrearCultivo(_laPlanta, _laHuerta, fechaSiembraSelec, idCultivo);
            if (addCultivo)
            {
                await DisplayAlert("Registro correcto", "Los datos se han completado correctamente", "Ok");
                await Navigation.PushAsync(new VerHuerta(_laHuerta));
            }
            else
            {
                await DisplayAlert("Registro incorrecto", "Revise los datos ingresados", "Cancelar");
            }
        }
    }
}