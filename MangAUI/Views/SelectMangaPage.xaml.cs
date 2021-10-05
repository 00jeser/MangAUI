using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System;
using MangAUI.Models;
using System.Collections.Generic;
using MangAUI.ViewModels;

namespace MangAUI.Views
{
    public partial class SelectMangaPage : ContentPage
    {
        int count = 0;

        public SelectMangaPage()
        {
            InitializeComponent();
            BindingContext = new SelectMangaViewModel(Navigation);
            Title = "Выбор манги";
            
        }

        public async void press(object sender, object aargs) 
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectChapterPage((MangaCard)((TappedEventArgs)e).Parameter));
        }
    }
}
