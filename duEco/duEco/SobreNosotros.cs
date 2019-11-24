using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace duEco
{
    public class SobreNosotros : ContentPage
    {
        public SobreNosotros()
        {
            //Content = new StackLayout
            //{
            //    Children = {
            //        new Label { Text = "Welcome to Xamarin.Forms!" }
            //    }
            //};
            var title = new Label
            {
                Text = "Sobre Nosotros",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            var description = new Label
            {
                Text = "duEco busca construir un producto que sea de beneficio para la sociedad y de este modo resolver algunas problemáticas vinculadas con la seguridad alimentaria, la salud, la recreación y el ambiente."
            };

            var blogTitle = new Label
            {
                Text = "duEco",
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

        //    List<string> articles = new List<string> {
        //"CloudCakes raises $50m and leads the Cake-as-a-Service models",
        //"Top 10 Cities With the Best Cakes",
        //"CloudCakes CEO awarded for Food Innovation Award 2016"
      //};

            //ListView articlesView = new ListView
            //{
            //    ItemsSource = articles
            //};

            Content = new StackLayout
            {
                Padding = 30,
                Spacing = 10,
                Children = { title, description, blogTitle/*, articlesView*/ }
            };

        }
    }
}