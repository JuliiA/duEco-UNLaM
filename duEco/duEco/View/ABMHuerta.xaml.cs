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
	public partial class ABMHuerta : ContentPage
	{
        private string _huertaId; 
		public ABMHuerta (string huertaID)
		{
			InitializeComponent();

            _huertaId = huertaID;
            if(_huertaId != "0")
            {
                Model.HuertaModel huertaEdit = HuertaServicio.BuscarPorId(_huertaId);
                if(huertaEdit != null)
                {
                    NombreHuerta.Text = huertaEdit.Nombre;
                    DescHuerta.Text = huertaEdit.Descripcion;
                }
            }

            btnActualizar.Clicked += btnActualizar_Clicked;
		}

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            var userLog = App.Current.Properties["user"].ToString();
            var ok = false;
            if (userLog != null) {

                if (_huertaId == "0")
                {
                    ok = HuertaServicio.CrearHuerta(NombreHuerta.Text, DescHuerta.Text, userLog);                    
                }
                else
                {
                    ok = HuertaServicio.ModificarHuerta(NombreHuerta.Text, DescHuerta.Text, _huertaId);
                }
                if (ok)
                {
                    await DisplayAlert("Registro correcto", "Los datos se han completado correctamente", "Ok");
                    await Navigation.PushAsync(new MisHuertas());
                }
                else
                {
                    await DisplayAlert("Registro incorrecto", "Revise los datos ingresados", "Cancelar");
                }
            }

        }
    }
}