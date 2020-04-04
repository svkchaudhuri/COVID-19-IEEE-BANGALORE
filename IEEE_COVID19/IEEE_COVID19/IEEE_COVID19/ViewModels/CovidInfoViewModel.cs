using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IEEE_COVID19.ViewModels.Base;
using Xamarin.Forms;

namespace IEEE_COVID19.ViewModels
{
    public class CovidInfoViewModel : ViewModelBase
    {
        private string _covidInfoChina;
        public string CovidInfoFromChina
        {
            get => _covidInfoChina;
            set
            {
                _covidInfoChina = value;
                RaisePropertyChanged(() => CovidInfoFromChina);
            }
        }

        public CovidInfoViewModel()
        {
            ClickMeCmd = new Command(ClickMe);
        }

        private void ClickMe(object obj)
        {
            CovidInfoFromChina = "This is latest info from china";
        }

        public ICommand ClickMeCmd { get; }

        //public override Task Initialize(object navigationData) { 
        //CovidInfoFromChina = "This is info from china";}
    }
}
