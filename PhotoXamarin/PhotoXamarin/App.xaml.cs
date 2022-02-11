using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using PhotoXamarin.db;

namespace PhotoXamarin
{
    public partial class App : Application
    {
        public const string DB_NAME = "PhotoObj.db";
        public static CRUDOperation db;
        public static CRUDOperation Db
        {
            get
            {
                if (db == null)
                {
                    db = new CRUDOperation(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DB_NAME));
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
