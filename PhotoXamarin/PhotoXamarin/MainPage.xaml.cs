using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PhotoXamarin.Models;
using PhotoXamarin.db;
using System.Diagnostics;
using System.IO;
using Xamarin.Essentials;

namespace PhotoXamarin
{
    public partial class MainPage : ContentPage
    {
        public string impath;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
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
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        async void GetPhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                impath = photo.FullPath;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private void ObjectList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ObjectsPage((ProjectPhoto)e.Item));
        }

        private void btnGallery_Clicked(object sender, EventArgs e)
        {

        }

        private void btnCam_Clicked(object sender, EventArgs e)
        {

        }

        public void UpdateList()
        {
            ObjectListview.ItemsSource = null;
            ObjectListview.ItemsSource = App.db.GetProjects();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateList();
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.db.SaveItem(new ProjectPhoto(name.Text, impath));
                DisplayAlert("", "Обьект успешно добавлен", "Ok");
                UpdateList();
            }
            catch
            {
                DisplayAlert("", "Не удалось добавить обьект", "Ok");
            }
        }
    }
}
