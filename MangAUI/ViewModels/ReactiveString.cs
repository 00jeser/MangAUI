using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangAUI.ViewModels
{
    public class ReactiveString : INotifyPropertyChanged
    {
        private string _v;
        public string ReactiveValue 
        {
            get => ReactiveValue;
            set 
            {
                _v = value;
                OnPropertyChanged(nameof(ReactiveValue));
            }
        }

        public ReactiveString(string value) 
        {
            ReactiveValue = value;
        }

        //------------------------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
