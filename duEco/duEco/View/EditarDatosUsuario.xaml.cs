using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using duEco.Servicio;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace duEco.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarDatosUsuario : ContentPage
	{
		public EditarDatosUsuario ()
		{
			InitializeComponent ();
            var userLog = App.Current.Properties["user"].ToString();
            var datos = Servicio.UsuarioServicio.Editar(userLog);
            if(datos != null)
            {
                txtNombre.Text = datos.nombre;
                txtEmail.Text = datos.email;
                txtPassword.Text = datos.password;
            }

            btnConfirmar.Clicked += btnConfirmar_Clicked;
        }

        private async void btnConfirmar_Clicked(object sender, EventArgs e)
        {
            var userLog = App.Current.Properties["user"].ToString();
            var ok = UsuarioServicio.Modificar(userLog, txtNombre.Text, txtEmail.Text, txtPassword.Text);
            if(ok)
            {
                await DisplayAlert("Actualizacion correcta", "Los datos se han actualizado correctamente", "Ok");
                await Navigation.PushAsync(new Index());

            }
            else
            {
                await DisplayAlert("Error", "No se pudieron actualizar los datos", "OK");
            }
        }
    }
}