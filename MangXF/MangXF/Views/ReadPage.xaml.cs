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
    public partial class ReadPage : ContentPage
    {
        public ReadPage(ChapterCard chapter)
        {
            InitializeComponent();
            BindingContext = new ReadViewModel(chapter);
            Title = chapter.name;
        }
    }
}