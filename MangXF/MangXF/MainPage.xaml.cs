using MangXF.Servises;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MangXF
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ApplicationSingleton.ApplicationWidth = (float)Application.Current.MainPage.Width;
        }
    }
}
