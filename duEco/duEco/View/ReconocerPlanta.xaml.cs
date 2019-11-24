using Acr.UserDialogs;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace duEco.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReconocerPlanta : ContentPage
	{
        private MediaFile _foto;

        public ReconocerPlanta()
		{
			InitializeComponent();
		}

        private async void Elegir_Click(object sender, EventArgs e)
        {
            bool hasPermission = false;

            try
            {
                await CrossMedia.Current.Initialize();
                hasPermission = CrossMedia.Current.IsPickPhotoSupported;
            }
            catch (Exception genEx)
            {
                var Error = genEx;
            }

            if (!hasPermission)
            {
                await DisplayAlert("Foto NO soportada", "No se ha podido acceder a la foto", "OK");
                return;
            }

            try
            {
                await CrossMedia.Current.Initialize();

                var foto = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions());

                _foto = foto;
                imgSource.Source = FileImageSource.FromFile(foto.Path);

                //_foto = await CrossMedia.Current.PickPhotoAsync();

                //if (_foto == null)
                //    return;
                ////image = file;
                //imgSource.Source = ImageSource.FromStream(() =>
                //{
                //    var stream = _foto.GetStream();
                //    return stream;
                //});
            }
            catch (Exception e2)
            {
                throw e2;
            }


        }

        private async void Tomar_Click(object sender, EventArgs e)
        {
            var opciones_almacenamiento = new StoreCameraMediaOptions()
            {
                SaveToAlbum = true,
                Name = "MiFoto.jpg"
            };

            _foto = await CrossMedia.Current.TakePhotoAsync(opciones_almacenamiento);
            imgSource.Source = ImageSource.FromStream(() =>
            {
                var stream = _foto.GetStream();
                _foto.Dispose();
                return stream;
            });


            //await CrossMedia.Current.Initialize();

            ////var obj = new StoreCameraMediaOptions();
            ////obj.Directory = "clasificador";
            ////obj.Name = "source.jpg";

            //try
            //{
            //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //    {
            //        UserDialogs.Instance.Toast("No camera available.");
            //        return;
            //    }

            //    var foto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            //    {
            //        DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
            //        PhotoSize = PhotoSize.Custom,
            //        CustomPhotoSize = 90,
            //        Directory = "Sample",
            //        Name = "test.jpg",
            //        SaveToAlbum = true
            //    });

            //    if (foto == null)
            //        return;

            //    await DisplayAlert("File Location", foto.Path, "OK");

            //    imgSource.Source = ImageSource.FromStream(() =>
            //    {
            //        var stream = foto.GetStream();
            //        return stream;
            //    });
            //    //var foto = await CrossMedia.Current.TakePhotoAsync(obj);
            //    //_foto = foto;
            //    //imgSource.Source = FileImageSource.FromFile(foto.Path);
            //}
            //catch (Exception x)
            //{

            //    throw x;
            //}

        }

        private async void Analizar_Click(object sender, EventArgs e)
        {
            try
            {
                const string endPoint = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/42edf9a9-9056-4f73-9a8d-1e5e068b4169/classify/iterations/Iteration1/image";
              // const string endPoint = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/69a2a9be-b469-4be6-8f72-3e9550b610e8/classify/iterations/Iteration1/image";

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Prediction-Key", "f8a96a7e93d94b55b1bede027874b2f0");

                var contentStream = new StreamContent(_foto.GetStream());

                using (UserDialogs.Instance.Loading("Analizando..."))
                {
                    contentStream.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    var response = await httpClient.PostAsync(endPoint, contentStream);

                    if (!response.IsSuccessStatusCode)
                    {
                        UserDialogs.Instance.Toast("Ha ocurrido un error al intenta analizar");
                        return;
                    }

                    var json = await response.Content.ReadAsStringAsync();

                    var prediction = JsonConvert.DeserializeObject<PredictionResponse>(json);
                    var tag = prediction.Predictions.FirstOrDefault();

                    Resultado.Text = $"{tag.TagName:p2} - {tag.Probability:p0}";
                    Precision.Progress = tag.Probability;
                }
            }
            catch (Exception ex)
            {
               throw ex;
            }

        }

        class PredictionResponse
        {
            public string Id { get; set; }
            public string Project { get; set; }
            public string Iteration { get; set; }
            public DateTime Created { get; set; }
            public Prediction[] Predictions { get; set; }
        }

        class Prediction
        {
            public string TagId { get; set; }
            public string TagName { get; set; }
            public float Probability { get; set; }
        }
    }
}