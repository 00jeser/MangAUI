using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MangXF
{
    class AutoSizableImage : Image
    {
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(propertyName == "Sourse") 
            {
                Debug.Write("sc");
            }
            base.OnPropertyChanged(propertyName);
        }
    }
}
