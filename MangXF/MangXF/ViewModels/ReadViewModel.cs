using MangXF.Models;
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
    class ReadViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;

        private ObservableCollection<string> chapters;
        public ObservableCollection<string> Images
        {
            get {  return chapters; }
            set 
            {
                chapters = value;
                OnPropertyChanged("Images");
            }
        }

        private string name;
        public string Name {  get {  return name; } set { name = value; OnPropertyChanged("Name"); } }

        private string url;

        public ReadViewModel(ChapterCard chapter) 
        {
            Images = new ObservableCollection<string>();
            url = chapter.url;
            Task.Run(Add);
        }

        private void Add() 
        {
            foreach(var c in (new Servises.Downloader(url)).GetImages()) 
            {
                Images.Add(c);
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
