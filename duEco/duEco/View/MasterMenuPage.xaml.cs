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
	public partial class MasterMenuPage : MasterDetailPage
	{
        INavigation Navigation;
		public MasterMenuPage ()
		{
			InitializeComponent ();
            Init();
		}

        private void Init()
        {
           
                List<Menu> menus = new List<Menu>
                {
                    new Menu { Text = App.Current.Properties["user"].ToString()},
                    new Menu { Text = "Home"},
                    new Menu { Text = "Mi perfil"},
                         new Menu { Text = "Cuenta premium"},
                              new Menu { Text = "Salir"},
                };
                    ListMenu.ItemsSource = menus;
                    ListMenu.Margin = new Thickness(0, 21, 0, 0);
                    Detail = new NavigationPage(new Home());
        }

        private void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var _menu = e.SelectedItem as Menu;
            var myPage = new Page();
            if (_menu != null)
            {
                switch (_menu.Text)
                {
                    case "Home":
                        myPage = new View.Home();
                        break;
                    case "Mi perfil":
                        myPage = new View.EditarDatosUsuario();
                        break;
                    case "Cuenta premium":
                        myPage = new View.CuentaPremium();
                        break;
                    default:
                        myPage = new Index();
                        App.Current.Properties["IsLoggedIn"] = false;
                        App.Current.Properties["user"] = null;
                        break;
                }

                Detail = new NavigationPage(myPage);
                this.IsPresented = false;
            }
        }
    }
}