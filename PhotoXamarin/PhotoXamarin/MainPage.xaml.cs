using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PhotoXamarin.Models;
using PhotoXamarin;
using PhotoXamarin.db;
using System.Diagnostics;
using System.IO;
using Xamarin.Essentials;

namespace PhotoXamarin
{
    public partial class MainPage : ContentPage
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public string impath;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateList();
        }

        async void btnGallery_Clicked(object sender, EventArgs e)
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

        async void btnCam_Clicked(object sender, EventArgs e)
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

        public void UpdateList()
        {
            ObjectList.RefreshCommand = new Command(() =>
            {
                ObjectList.IsRefreshing = false;
            });
            ObjectList.ItemsSource = null;
            /*Неизвестная ошибка*/
            ObjectList.ItemsSource = App.Db.GetProjectPhotos();
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.DB.SaveItem(new ProjectPhoto(Name.Text, impath));
                DisplayAlert("", "Обьект успешно добавлен", "Ok");
                UpdateList();
            }
            catch
            {
                DisplayAlert("", "Не удалось добавить обьект", "Ok");
            }
        }

        private void ObjectList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ObjectsPage((ProjectPhoto)e.Item));
        }

        private void btnSwipe_Clicked(object sender, EventArgs e)
        {
            try
            {
                var id = ((SwipeItem)sender).CommandParameter.ToString();
                App.Db.DeleteItem(int.Parse(id));
                UpdateList();
            }
            catch (Exception ex)
            {
                DisplayAlert("Не удалось удалить объект", ex.Message, "ok");
            }
        }
    }
}
