using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsoCamara.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Fotos : ContentPage
    {
        public Fotos()
        {
            InitializeComponent();
            this.btnFoto.Clicked += Btnfoto_Clicked;
        }

        private async void Btnfoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No hay camara", "No se detecta la camara", "Ok");
                    return;
                }
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    //variable para guardar la variable en el album publico
                    SaveToAlbum = true
                });
                if (file == null)
                    return;

                this.imgcamara.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                await DisplayAlert("Foto Capturada", "Localizacion: " + file.AlbumPath, "Ok");
            }catch(Exception ex)
            {
                await DisplayAlert("Permiso denegado", "De los permisos de camara al dispositivo. \nError: " + ex.Message, "Ok");
            }
        }
    }
}