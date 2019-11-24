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
	public partial class VerPlanta : ContentPage
	{
		public VerPlanta (string plantaId)
		{
			InitializeComponent ();

            var esPlanta = PlantaServicio.buscarPorId(plantaId);
            NombrePlanta.Text = esPlanta.nombre;
            Descripcion.Text = esPlanta.descripcion;
            lblDificultad.Text = esPlanta.dificultad;
            lblDistancia.Text = esPlanta.distancia;
            lblEpoca.Text = esPlanta.epocaSiembra;
            lblTipoSiembra.Text = esPlanta.tipoSiembra;
            lblValor.Text = esPlanta.valorNutricional;

            ImagenPortada.Source = Device.RuntimePlatform == Device.Android ?
                                    ImageSource.FromFile("iso_v7.png")
                                    : ImageSource.FromFile("Imagenes/iso_v7.PNG");
		}
	}
}