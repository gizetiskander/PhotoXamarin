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
    public partial class Objects : ContentPage
    {
        readonly ProjectPhoto im;
        public Objects()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.im = im;
            name.Text = im.Name;
            image.Source = im.Image;
        }
    }
}