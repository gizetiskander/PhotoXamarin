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
        string impath;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void ObjectList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new Objects((ProjectPhoto)e.Item));
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
