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
	public partial class VerHuerta : ContentPage
	{
        private string _huertaID;

        public VerHuerta (string huertaID)
		{
			InitializeComponent ();
            _huertaID = huertaID;
            MostrarHuertaSeleccionada(_huertaID);

            btnAgregar.Clicked += BtnAgregar_Clicked;
            btnEditar.Clicked += BtnEditar_Clicked;
            btnTodas.Clicked += BtnTodas_Clicked;
            btnBorrar.Clicked += BtnBorrar_Clicked;
        }

        private async void BtnBorrar_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Confirmar la eliminacion", "Seguro quiere eliminar?", "Si", "No");
            if (answer)
            {
                var ok = HuertaServicio.Eliminar(_huertaID);
                if (ok)
                {
                    await DisplayAlert("Info!", "La huerta se ha eliminado correctamente", "Ok");
                    await Navigation.PushAsync(new MisHuertas());
                }
                else
                {
                    await DisplayAlert("Info!", "No se ha podido eliminar la huerrta", "Cancelar");
                }
            }
        }

        private void BtnEditar_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new ABMHuerta(_huertaID));
        }

        private void BtnTodas_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MisHuertas());
        }

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new ABMCultivo(_huertaID));
        }

        private void MostrarHuertaSeleccionada(string huertaID)
        {
            var esHuerta = HuertaServicio.BuscarPorId(huertaID);
            if(esHuerta != null)
            {
                NombreHuerta.Text = esHuerta.Nombre;
                DescripcionHuerta.Text = esHuerta.Descripcion;

                if(esHuerta.ListaCultivos.Count > 0)
                {
                    var cultivoTest = new Model.CultivoModel();
                    foreach (var item in esHuerta.ListaCultivos)
                    {
                        if (item != null)
                        {
                            item.descripcion = "% Sembrado: 20";
                            item.porcentaje = 0.2f;// 1.0 es el 100%

                            // Difference in days, hours, and minutes.
                            TimeSpan ts = DateTime.Now - item.IniciaCultivo;

                            // Difference in days.
                            int differenceInDays = ts.Days;

                            if (differenceInDays > 180)
                            {
                                item.porcentaje = 1.0f;
                                item.descripcion = "% Sembrado: 100";
                            }
                            else
                            {
                                if(differenceInDays > 150)
                                {
                                    item.porcentaje = 0.8f;
                                    item.descripcion = "% Sembrado: 80";
                                }
                                else
                                {
                                    if (differenceInDays > 90)
                                    {
                                        item.porcentaje = 0.5f;
                                        item.descripcion = "% Sembrado: 50";
                                    }
                                }

                            }

                            //if (item.IniciaCultivo.Month < 3)
                            //{
                            //    item.porcentaje = 0.8f;
                            //    item.descripcion = "% Sembrado: 80";
                            //}
                            //else
                            //{
                            //    if (item.IniciaCultivo.Month < 6)
                            //    {
                            //        item.porcentaje = 0.5f;
                            //        item.descripcion = "% Sembrado: 50";
                            //    }
                            //}
                        }
                    }
                    
                    lstCultivos.ItemsSource = esHuerta.ListaCultivos;
                }
            }
        }

        private void LstCultivos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedCultivo = (Model.CultivoModel)e.SelectedItem;
            var item = selectedCultivo.IdCultivo.ToString();
            try
            {
                if (selectedCultivo != null)
                {
                    ((NavigationPage)this.Parent).PushAsync(new VerCultivo(item));
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}