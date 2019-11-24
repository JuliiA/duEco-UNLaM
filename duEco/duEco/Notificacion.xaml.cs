using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace duEco
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Notificacion : ContentPage
	{
		public Notificacion ()
		{
			InitializeComponent ();
            btnNotification.Clicked += BtnNotification_Clicked;
        }

        void BtnNotification_Clicked(object sender, EventArgs e)
        {
            CrossLocalNotifications.Current.Show("Xamarin Latino", "Hello, this is a local notification from Xamarin Forms", 0, DateTime.Now.AddSeconds(5));
            //CrossLocalNotifications.Current.Show("Xamarin Latino", "Hello, this is a local notification from Xamarin Forms", 0, new DateTime(2019,11,3,18,43,00));

        }

        private void BtnCancelarNotificacion_Clicked(object sender, EventArgs e)
        {
            CrossLocalNotifications.Current.Cancel(0);
        }
    }
}