using MangXF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangXF.ViewModels
{
    class SelectChapterViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ChapterCard> chapters;
        public ObservableCollection<ChapterCard> Chapters
        {
            get {  return chapters; }
            set 
            {
                chapters = value;
                OnPropertyChanged("Chapters");
            }
        }

        private string name;
        public string Name {  get {  return name; } set { name = value; OnPropertyChanged("Name"); } }

        private string url;

        public SelectChapterViewModel(MangaCard manga) 
        {
            Name = manga.title;
            url = manga.url;
            Chapters = new ObservableCollection<ChapterCard>();
            Task.Run(Add);
        }

        private void Add() 
        {
            foreach(var c in (new Servises.Downloader(url)).GetChaptersList()) 
            {
                Chapters.Add(c);
            }
        }


        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
