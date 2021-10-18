using MangXF.Models;
using MangXF.Servises;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MangXF.ViewModels
{
    class ReadViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ImageModel> chapters;
        public ObservableCollection<ImageModel> Images
        {
            get { return chapters; }
            set
            {
                chapters = value;
                OnPropertyChanged("Images");
            }
        }

        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

        private bool _isCarousel = false;
        public bool isCarousel { get { return _isCarousel; } set { _isCarousel = value; OnPropertyChanged("isCarousel"); } }

        private string url;

        public ReadViewModel(ChapterCard chapter)
        {
            Images = new ObservableCollection<ImageModel>();
            url = chapter.url;
            Init();
        }


        private async void Init()
        {
            await Task.Run(Add);
        }

        private void Add()
        {
            foreach (var c in new Downloader(url).GetImages())
                Device.BeginInvokeOnMainThread(() =>
                {
                    Images.Add(c);
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
