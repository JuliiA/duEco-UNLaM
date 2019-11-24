using duEco.Model;
using duEco.Servicio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace duEco.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Catalogo : ContentPage
	{
        public Catalogo()
        {
            InitializeComponent();

            cargarCatalogo();
            listarCategorias();
            
        }

        private void listarCategorias()
        {
            List<Model.CategoriaModel> lasCategorias = PlantaServicio.obtenerCategorias(); //new List<Model.CategoriaModel>();

            foreach (Model.CategoriaModel categoria in lasCategorias)
            {
                ddlCategorias.Items.Add(categoria.nombre);
            }
        }

        private void cargarCatalogo()
        {
            var plantasCatalogo = PlantaServicio.todasLasPlantas();
            listarEnCatalogo(plantasCatalogo);
        }

        private void listarEnCatalogo(List<PlantaModel> plantasCatalogo)
        {
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
                    ((NavigationPage)this.Parent).PushAsync(new VerPlanta(item));
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            
        }

        private void DdlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemSelec = ddlCategorias.SelectedIndex;
            var ddlitems = ddlCategorias.Items;
            var plantasCatalogoFiltro = PlantaServicio.obtenerByCategoria(itemSelec);
            listarEnCatalogo(plantasCatalogoFiltro);
        }
    }
}