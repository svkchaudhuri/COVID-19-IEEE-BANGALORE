using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IEEE_COVID19.ViewModels.Base;

namespace IEEE_COVID19.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoVizView : ContentPage
    {
        public CoVizView()
        {
            InitializeComponent();
        }
    }
}