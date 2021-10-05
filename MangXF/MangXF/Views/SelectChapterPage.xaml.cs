using MangXF.Models;
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
    public partial class SelectChapterPage : ContentPage
    {
        public SelectChapterPage(MangaCard manga)
        {
            InitializeComponent();
            BindingContext = new SelectChapterViewModel(manga);
            manga.title = manga.title.Replace('\n', ' ');
            if (manga.title.Length > 40)
                Title = $"Выбор главы - {manga.title.Substring(0, 40)}...";
            else
                Title = $"Выбор главы - {manga.title}";

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReadPage((ChapterCard)((TappedEventArgs)e).Parameter));
        }
    }
}