using MangAUI.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangAUI.ViewModels
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

        public SelectChapterViewModel(MangaCard manga) 
        {
            Name = manga.title;
            Chapters = new ObservableCollection<ChapterCard>((new Servises.Downloader(manga.url)).GetChaptersList());
        }


        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
