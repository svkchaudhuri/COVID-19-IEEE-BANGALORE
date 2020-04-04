using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IEEE_COVID19.Models
{
    public class AppMenuItem : BindableObject
    {
        private bool _isSelected;
        public string IconName { get; set; }
        public string MenuName { get; set; }
        public Action NavigationAction { get; set; }
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged("IsSelected"); }
        }
    }
}
