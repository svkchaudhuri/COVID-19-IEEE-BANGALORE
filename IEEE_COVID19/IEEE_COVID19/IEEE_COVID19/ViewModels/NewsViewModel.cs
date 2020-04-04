using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using IEEE_COVID19.Services.NavigationService;
using IEEE_COVID19.ViewModels.Base;
using Xamarin.Forms;

namespace IEEE_COVID19.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        //Declare Commands here


        public ICommand WhoNewsCmd { get; }
        public ICommand MohNewsCmd { get; }
        public ICommand IndiaGovCmd { get; }
        public ICommand TwitterfeedCmd { get; }
        public ICommand TwittermohCmd { get; }
        public ICommand AnimalCmd { get; }
        public ICommand DietCmd { get; }
        public NewsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            WhoNewsCmd = new Command(NavigateToWhoNews);
            MohNewsCmd = new Command(NavigateToMohNews);
            IndiaGovCmd = new Command(NavigateToIndiaGovNews);
            TwitterfeedCmd = new Command(NavigateToTwitterfeed);
            TwittermohCmd = new Command(NavigateToTwittermoh);
            AnimalCmd = new Command(NavigateToAnimal);
            DietCmd = new Command(NavigateToDiet);

        }

        private void NavigateToWhoNews(object obj)
        {
            _navigationService.NavigateToAsync<NewsDetailViewModel>("https://www.who.int/emergencies/diseases/novel-coronavirus-2019");
        }

        private void NavigateToMohNews(object obj)
        {
            _navigationService.NavigateToAsync<NewsDetailViewModel>("https://www.mohfw.gov.in/");
        }

        private void NavigateToIndiaGovNews(object obj)
        {
            _navigationService.NavigateToAsync<NewsDetailViewModel>("https://www.who.int/emergencies/diseases/novel-coronavirus-2019/technical-guidance/points-of-entry-and-mass-gatherings");
        }
        private void NavigateToTwitterfeed(object obj)
        {
            _navigationService.NavigateToAsync<NewsDetailViewModel>("https://twitter.com/who?lang=en");
        }



        private void NavigateToAnimal(object obj)
        {
            _navigationService.NavigateToAsync<NewsDetailViewModel>("https://www.who.int/health-topics/coronavirus/who-recommendations-to-reduce-risk-of-transmission-of-emerging-pathogens-from-animals-to-humans-in-live-animal-markets");
        }

        private void NavigateToTwittermoh(object obj)
        {
            _navigationService.NavigateToAsync<NewsDetailViewModel>("https://twitter.com/MoHFW_INDIA");
        }

        private void NavigateToDiet(object obj)
        {
            _navigationService.NavigateToAsync<NewsDetailViewModel>("http://www.euro.who.int/en/health-topics/health-emergencies/coronavirus-covid-19/novel-coronavirus-2019-ncov-technical-guidance/food-and-nutrition-tips-during-self-quarantine");
        }


    }
}




