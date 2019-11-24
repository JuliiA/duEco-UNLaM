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
	public partial class ABMCultivo : ContentPage
	{
        private string _laHuertaId;
		public ABMCultivo (string huertaID)
		{
			InitializeComponent ();
            _laHuertaId = huertaID;
            cargarCatalogo();
        }

        private void cargarCatalogo()
        {
            var plantasCatalogo = PlantaServicio.todasLasPlantas();
            foreach (var item in plantasCatalogo)
            {
                item.imagenPortada = Device.RuntimePlatform == Device.Android ?
                        ImageSource.FromFile("isoduEco.png").ToString()
                        : ImageSource.FromFile("Imagenes/isoduEco.PNG").ToString();
            }

            lstPlantas.ItemsSource = plantasCatalogo;

        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPlanta = (Model.PlantaModel)e.SelectedItem;
            var item = selectedPlanta.id.ToString();
            try
            {
                if (selectedPlanta != null)
                {
                    ((NavigationPage)this.Parent).PushAsync(new CultivoCalendario(item, _laHuertaId));
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}