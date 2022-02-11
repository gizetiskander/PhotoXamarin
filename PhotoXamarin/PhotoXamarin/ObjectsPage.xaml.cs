using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PhotoXamarin.db;
using PhotoXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectsPage : ContentPage
    {
        readonly ProjectPhoto im;
        public ObjectsPage(ProjectPhoto im)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            name.Text = im.Name;
            image.Source = im.Image;
        }
    }
}