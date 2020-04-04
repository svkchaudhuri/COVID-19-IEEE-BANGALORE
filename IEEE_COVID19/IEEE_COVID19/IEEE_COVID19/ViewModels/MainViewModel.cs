using System.Threading.Tasks;
using System.Windows.Input;
using IEEE_COVID19.Models;
using IEEE_COVID19.Services.NavigationService;
using IEEE_COVID19.ViewModels.Base;
using Plugin.Connectivity;
using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Messaging;
using IEEE_COVID19.Services.SettingsService;

namespace IEEE_COVID19.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private MenuViewModel _menuViewModel;
        private readonly INavigationService _navigationService;
        private ISettingsService _iSettingsService;
        public ICommand LinkOpen { get; }
        public MenuViewModel MenuViewModel
        {
            get { return _menuViewModel; }
            private set { _menuViewModel = value; RaisePropertyChanged(() => MenuViewModel); }
        }

        public MainViewModel(INavigationService navigationService, ISettingsService settingsService)
        {
            _navigationService = navigationService;
            LinkOpen = new Command(PhoneCall);
            _iSettingsService = settingsService;
        }

        private void PhoneCall(object obj)
        {

            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                if (_iSettingsService.FillSettingsDetails())
                    phoneCallTask.MakePhoneCall(_iSettingsService.ApplicationSettings.Contact1, "Local Contact");
                else
                {
                    phoneCallTask.MakePhoneCall("+911123978046", "Central Coronavirus HelpLine");
            }


            }
        }

        public override async Task Initialize(object navigationParameter)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                var errorData = new ErrorDetails()
                {
                    ErrorMessage = "Internet is not available",
                    NavigationBackAction = async (parameter) => { await _navigationService.NavigateToAsync<MainViewModel>(parameter); },
                    NavigationData = string.Empty
                };
                await _navigationService.NavigateToAsync<ErrorViewModel>(errorData);
            }
            else
            {
                MenuViewModel = ViewModelLocator.Resolve<MenuViewModel>();
                await MenuViewModel.Initialize(navigationParameter);
            }
        }
    }
}
