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
	public partial class VerCultivo : ContentPage
	{
		public VerCultivo (string cultivoID)
		{
			InitializeComponent();
            MostrarCultivoSeleccionado(cultivoID);
		}

        private void MostrarCultivoSeleccionado(string cultivoID)
        {
            var esCultivo = CultivoServicio.BuscarPorId(cultivoID);
            if (esCultivo != null)
            {
                NombreCultivo.Text = esCultivo.NombreCultivo;
                lblFecha.Text = "El cultivo se sembro: " + esCultivo.IniciaCultivo.ToShortDateString();
                lblEstado.Text = esCultivo.IniciaCultivo.Month < 6 ? "Cosechado" : "En tratamiento";
                List <Model.CultivoModel.TareasEtapas> obtenerTareas = CultivoServicio.TraerTareas(esCultivo.id);
                if(obtenerTareas.Count > 0)
                {
                    lstTareas.ItemsSource = obtenerTareas;
                }
            }
        }
    }
}