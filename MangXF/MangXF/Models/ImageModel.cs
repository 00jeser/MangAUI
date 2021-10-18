using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MangXF.Models
{
    public class ImageModel
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public string ImageUrl { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public ImageSource Url
        {
            get
            {
                if (ImageUrl == "MangXF.notfound.png")
                {
                    return ImageSource.FromResource("MangXF.notfound.png", System.Reflection.Assembly.GetExecutingAssembly());
                }
                else
                {
                    return ImageSource.FromUri(new Uri(ImageUrl));
                }
            }
        }

        public ImageModel(float width, float height, string url)
        {
            Width = width;
            Height = height;
            ImageUrl = url;
        }
        public ImageModel(bool isNull)
        {
            Width = (float)Application.Current.MainPage.Width;
            Height = ((float)Application.Current.MainPage.Width / 1128) * 1712;
            ImageUrl = "MangXF.notfound.png";
            //Url = ImageSource.FromResource("notfound.png", System.Reflection.Assembly.GetExecutingAssembly());
        }
        public ImageModel()
        {
        }
    }
}
