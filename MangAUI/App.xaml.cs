using MangAUI.Servises;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Application = Microsoft.Maui.Controls.Application;
using MangAUI.Views;

namespace MangAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new SelectMangaPage());
            //(new Downloader("http://www.mangarussia.com/manga/Доктор+Стоун.html")).Download();
        }
    }
}
