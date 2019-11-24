using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using duEco.Model;
//using RestSharp; --TODO
using Newtonsoft.Json;

using Xamarin.Forms;

namespace duEco
{
    public class Index : ContentPage
    {
        private Model.UsuarioModel usuarioLogin;
       
        Label title = new Label
        {
            Text = "Bienvenido a duEco",
            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            HorizontalOptions = LayoutOptions.CenterAndExpand,
        };
        
        Image icon = new Image
        {
            Source = Device.RuntimePlatform == Device.Android ? 
                        ImageSource.FromFile("isoduEco.png") 
                        : ImageSource.FromFile("Imagenes/isoduEco.PNG"),
            HeightRequest = 140,
            WidthRequest = 140,
            BackgroundColor = Color.Transparent            
        };

        Entry email = new Entry
        {
            Text = String.Empty,
            Placeholder = "E-Mail"
        };

        Entry password = new Entry
        {
            Text = String.Empty,
            Placeholder = "Contraseña",
            IsPassword = true
        };

        public Index()
        {
            MasterDetailPage master = new MasterDetailPage();
            master.IsPresented = false;
            master.IsGestureEnabled = false;
            
            #region UsuarioDePrueba
            Model.UsuarioModel userTest = new Model.UsuarioModel
            {
                nombre = "John Doe",
                email = "john.d@mail.com",
                password = "1234"
            };
            #endregion

            StackLayout stackLayout = new StackLayout();
            stackLayout.Padding = 30;
            stackLayout.Spacing = 10;

            stackLayout.Children.Add(title);            
            stackLayout.Children.Add(icon);
            stackLayout.Children.Add(email);            
            stackLayout.Children.Add(password);

            var login = new Button
            {
                Text = "Login",
                BackgroundColor = Color.MediumSeaGreen,
                TextColor = Color.White,
                BorderWidth = 20
            };
            login.Clicked += OnButtonClicked;
            stackLayout.Children.Add(login);

            var aboutButton = new Button
            {
                Text = "Sobre nosotros"
            };
            stackLayout.Children.Add(aboutButton);

            var signupButton = new Button
            {
                Text = "Registro"
            };

            // Here we are implementing a click event using lambda expressions
            // when a user clicks the `aboutButton` they will navigate to the
            // About Us page.
            aboutButton.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new SobreNosotros());
            };

            // Navigation to the Signup Page (Note: We haven't created this page yet)
            signupButton.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new Registro());
            };
            stackLayout.Children.Add(signupButton);
                   
            var underlineLabel = new Label
            {
                Text = "Si no posee cuenta Registrese",
                TextDecorations = TextDecorations.Underline,
                TextColor = Color.DarkRed
            };
            stackLayout.Children.Add(underlineLabel);
            stackLayout.BackgroundColor = Color.Transparent;

            Content = stackLayout;
            NavigationPage.SetHasNavigationBar(this, false);
        }       

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            usuarioLogin = new Model.UsuarioModel
            {
                email = email.Text,
                password = password.Text
            };
            if (validarUsuario(usuarioLogin))
            {
                App.Current.Properties["user"] = usuarioLogin.email;
                App.Current.Properties["IsLoggedIn"] = true;
                App.Current.MainPage = new View.MasterMenuPage();
                await Navigation.PushAsync(new View.Home());
            }
            else
            {
                await DisplayAlert("Error", "Email y/o password incorrectos", "OK");
            }
        }

        private bool validarUsuario(UsuarioModel usuarioLogin)
        {
            return Servicio.UsuarioServicio.UsuarioExiste(usuarioLogin);
        }
    }
}