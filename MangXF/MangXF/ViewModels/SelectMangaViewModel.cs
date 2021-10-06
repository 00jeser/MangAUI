﻿using MangXF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MangXF.ViewModels
{
    class SelectMangaViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;

        private ObservableCollection<MangaCard> mangas;
        public ObservableCollection<MangaCard> Mangas
        {
            get { return mangas; }
            set
            {
                mangas = value;
                OnPropertyChanged("Mangas");
            }
        }


        private ObservableCollection<MangaCard> searchmangas;
        public ObservableCollection<MangaCard> SearchMangas
        {
            get { return searchmangas; }
            set
            {
                searchmangas = value;
                OnPropertyChanged("SearchMangas");
            }
        }

        public SelectMangaViewModel(INavigation navigation) 
        {
            Navigation = navigation;
            Mangas = new ObservableCollection<MangaCard>((new Servises.Downloader("https://www.mangarussia.com/")).GetMainMangaList().ToList().Take(10));
            SearchMangas = new ObservableCollection<MangaCard>(Mangas);
        }


        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
