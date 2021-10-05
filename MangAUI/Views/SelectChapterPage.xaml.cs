using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System;
using MangAUI.Models;
using System.Collections.Generic;
using MangAUI.ViewModels;

namespace MangAUI.Views
{
    public partial class SelectChapterPage : ContentPage
    {
        int count = 0;

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

        public async void press(object sender, object aargs)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReadPage((ChapterCard)((TappedEventArgs)e).Parameter));
        }
    }
}
