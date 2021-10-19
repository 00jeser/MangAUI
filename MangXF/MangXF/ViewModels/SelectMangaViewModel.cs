using MangXF.Models;
using MangXF.Servises;
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
        private ObservableCollection<MangaCard> lastManga;
        public ObservableCollection<MangaCard> LastManga
        {
            get { return lastManga; }
            set
            {
                lastManga = value;
                OnPropertyChanged("LastManga");
            }
        }

        private Command _search;
        public Command Search
        {
            get {  return _search; }
            set {  _search = value; OnPropertyChanged(nameof(Search)); }
        }

        private string _searchString;

        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; OnPropertyChanged(nameof(SearchString)); }
        }


        public SelectMangaViewModel(INavigation navigation) 
        {
            Navigation = navigation;
            Mangas = new ObservableCollection<MangaCard>(new Downloader("https://www.mangarussia.com/").GetMainMangaList().ToList().Take(10));
            SearchMangas = new ObservableCollection<MangaCard>(Mangas);
            LastManga = new ObservableCollection<MangaCard>(new Downloader("").GetLastMangaList());
            Search = new Command(() => {
                SearchMangas = new ObservableCollection<MangaCard>(new Servises.Downloader("").FindMangaList(SearchString).ToList());
            });
        }


        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
