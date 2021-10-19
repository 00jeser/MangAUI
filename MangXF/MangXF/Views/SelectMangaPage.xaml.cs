using MangXF.Models;
using MangXF.Servises;
using MangXF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectMangaPage : ContentPage
    {
        public SelectMangaPage()
        {
            InitializeComponent();
            BindingContext = new SelectMangaViewModel(Navigation);
            Title = "Выбор манги";
            LastLabel.Text = "Последние обновления";
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Downloader.AddLastManga((MangaCard)((TappedEventArgs)e).Parameter);
            await Navigation.PushAsync(new SelectChapterPage((MangaCard)((TappedEventArgs)e).Parameter));
        }
    }
}