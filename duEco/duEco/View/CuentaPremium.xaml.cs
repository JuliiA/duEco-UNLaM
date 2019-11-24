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
	public partial class CuentaPremium : ContentPage
	{
		public CuentaPremium ()
		{
			InitializeComponent ();
            btnActualizar.Clicked += btnActualizar_ClickedAsync;
		}

        private async void btnActualizar_ClickedAsync(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("duEco Premium", "Confirma actualizacion de Cuenta?", "No, por ahora", "Si");
            if (resp)
            {
                await Navigation.PushAsync(new Home());
            }
        }
    }
}