﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IEEE_COVID19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonateView : ContentPage
    {
        public DonateView()
        {
            InitializeComponent();
        }

        private void StatewiseCMReliefFund_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PickerView());
        }
    }
}
