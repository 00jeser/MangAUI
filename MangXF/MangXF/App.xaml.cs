using MangXF.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangXF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SelectMangaPage());
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
