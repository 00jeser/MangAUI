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
        bool nav = false;
        public ReadPage(ChapterCard chapter)
        {
            InitializeComponent();
            BindingContext = new ReadViewModel(chapter);
            Title = chapter.name;
            NavigationPage.SetHasNavigationBar(this, nav);
            //ToolbarItems.Add(new ToolbarItem("Изм. вид", "feed.png", () =>
            //{
            //    ((ReadViewModel)BindingContext).isCarousel = !((ReadViewModel)BindingContext).isCarousel;
            //}));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            nav = !nav;
            NavigationPage.SetHasNavigationBar(this, nav);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            ((ReadViewModel)BindingContext).isCarousel = !((ReadViewModel)BindingContext).isCarousel;
        }
    }
}