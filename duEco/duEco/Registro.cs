using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace duEco
{
    public class Registro : ContentPage
    {
        public Registro()
        {
            StackLayout stackLayout = new StackLayout();
            stackLayout.Padding = 30;
            stackLayout.Spacing = 10;

            var msj = new Label { Text = "Nueva Cuenta en duEco", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), HorizontalOptions = LayoutOptions.Center };
            stackLayout.Children.Add(msj);
            // A new element we're creating here - the Entry element
            // Entry allows us to capture user input
            // We are adding a Placeholder attribute to tell the user
            // which data we want them to enter
            var email = new Entry
            {
                Placeholder = "Email"
            };
            stackLayout.Children.Add(email);
            // Similar to the email entry button, we capture the
            // users password here. To hide the password from being
            // displayed we set the `IsPassword` attribute to true
            var password = new Entry
            {
                Placeholder = "Contraseña",
                IsPassword = true
            };
            stackLayout.Children.Add(password);

            var signupButton = new Button
            {
                Text = "Registrarse",
                BackgroundColor = Color.MediumSeaGreen,
                TextColor = Color.White,
                BorderWidth = 20
            };
           
            signupButton.Clicked += (object sender, EventArgs e) => {

                registrarNuevoUsuario(email.Text, password.Text);
                
            };
            stackLayout.Children.Add(signupButton);
            stackLayout.BackgroundColor = Color.Transparent;

            Content = stackLayout;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void registrarNuevoUsuario(string text1, string text2)
        {
            if (!String.IsNullOrEmpty(text1) && !String.IsNullOrEmpty(text2))
            {
                if (Servicio.UsuarioServicio.Registrar(text1, text2))
                {
                    await DisplayAlert("Registro correcto", "Los datos se han completado correctamente", "Ok");
                    await Navigation.PushAsync(new Index());

                }
                else
                {
                    await DisplayAlert("Error", "No se pudieron registrar los datos", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Complete ambos campos correctamente", "OK");
            }
        }
    }
}