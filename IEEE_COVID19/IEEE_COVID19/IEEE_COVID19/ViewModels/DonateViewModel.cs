using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using IEEE_COVID19.Services.NavigationService;
using IEEE_COVID19.ViewModels.Base;
using Xamarin.Forms;

namespace IEEE_COVID19.ViewModels
{
    public class DonateViewModel : ViewModelBase
    {
       

        private readonly INavigationService _navigationService;
        //Declare Commands here


        public ICommand PmcaresCmd { get; }
        //public ICommand CmrelieffundCmd { get; }


        public DonateViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            PmcaresCmd = new Command(NavigateToPmcares);
            //CmrelieffundCmd = new Command(NavigateToCmrelief);
        }


            private void NavigateToPmcares(object obj)
            {
                _navigationService.NavigateToAsync<DonateDetailsViewModel>("https://www.pmindia.gov.in/en/news_updates/appeal-to-generously-donate-to-pms-citizen-assistance-and-relief-in-emergency-situations-fund-pm-cares-fund/");
            }




    }
}

