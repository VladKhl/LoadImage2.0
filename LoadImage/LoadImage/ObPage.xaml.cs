using LoadImage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoadImage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObPage : ContentPage
    {
        readonly ImageModel im;
        string impath;
        public ObPage(ImageModel im)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.im = im;
            name.Text = im.Name;
            img.Source = im.Image;
        }

        private void editname_Clicked(object sender, EventArgs e)
        {
            name.IsEnabled = true;
        }

        async void GetPhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                impath = photo.FullPath;
                img.Source = impath;
                save.IsEnabled = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private void save_Clicked(object sender, EventArgs e)
        {
            try
            {
                im.Name = name.Text;
                im.Image = impath;
                App.db.SaveItem(im);
                DisplayAlert("", "Успешно изменено", "OK");
            }
            catch
            {
                DisplayAlert("Сообщение об ошибке", "Ошибка редактирования", "OK");
            }
        }

        async void TakePhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });
                var newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                Debug.WriteLine($"Путь фото {photo.FullPath}");
                impath = photo.FullPath;
                img.Source = impath;
                save.IsEnabled = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}