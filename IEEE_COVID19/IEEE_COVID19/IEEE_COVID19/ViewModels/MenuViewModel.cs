using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FontAwesome;
using IEEE_COVID19.Models;
using IEEE_COVID19.Services.MessageService;
using IEEE_COVID19.Services.NavigationService;
using IEEE_COVID19.Services.SettingsService;
using IEEE_COVID19.ViewModels.Base;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace IEEE_COVID19.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private AppMenuItem _selectedMenu;
        private INavigationService _navigationService;
        private ISettingsService _settingsService;
        private IAlertMessageService _alertMessageService;
        public List<AppMenuItem> AppMenus { get; }
        public AppMenuItem SelectedMenu
        {
            get { return _selectedMenu; }
            set
            {
                _selectedMenu = value;
                RaisePropertyChanged(() => SelectedMenu);
            }
        }
        public ICommand CmdNavigateMenu { get; }

        public MenuViewModel(INavigationService navigationService, ISettingsService settingsService, IAlertMessageService alertMessageService)
        {
            _navigationService = navigationService;
            _settingsService = settingsService;
            _alertMessageService = alertMessageService;
            CmdNavigateMenu = new Command<AppMenuItem>(NavigateMenu);
            AppMenus = new List<AppMenuItem>
            {
                new AppMenuItem
                {
                    MenuName = ConstantStrings.HomeMenuName,
                    IconName = FontAwesomeIcons.Home,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<HomeViewModel>(string.Empty)
                },
                new AppMenuItem
                {
                    MenuName = ConstantStrings.NewsMenuName,
                    IconName = FontAwesomeIcons.Newspaper,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<NewsViewModel>(string.Empty)
                }
                ,
                new AppMenuItem
                {
                    MenuName = ConstantStrings.CovidInfoMenuName,
                    IconName = FontAwesomeIcons.InfoCircle,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<CovidInfoViewModel>(string.Empty)
                }
                 ,
                 new AppMenuItem
                {
                    MenuName = ConstantStrings.SettingsMenuName,
                    IconName = FontAwesomeIcons.Cog,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<SettingsViewModel>(string.Empty)
                }
                 ,
                 new AppMenuItem
                {
                    MenuName = ConstantStrings.CoVizMenuName,
                    IconName = FontAwesomeIcons.GlobeAsia,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<CoVizViewModel>(string.Empty)
                },
                 new AppMenuItem
                {
                    MenuName = ConstantStrings.GuidanceMenuName,
                    IconName = FontAwesomeIcons.Directions,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<GuidanceViewModel>(string.Empty)
                },
                 new AppMenuItem
                {
                    MenuName = ConstantStrings.DonateMenuName,
                    IconName = FontAwesomeIcons.Donate,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<DonateViewModel>(string.Empty)
                },
                 new AppMenuItem
                {
                    MenuName = ConstantStrings.SymptomaticTestMenuName,
                    IconName = FontAwesomeIcons.NotesMedical,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<SymptomaticTestViewModel>(string.Empty)
                },
                  new AppMenuItem
                {
                    MenuName = ConstantStrings.WorkfromhomeMenuName,
                    IconName = FontAwesomeIcons.Laptop,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<WorkfromhomeViewModel>(string.Empty)
                },
                   new AppMenuItem
                {
                    MenuName = ConstantStrings.ImportantNoticeMenuName,
                    IconName = FontAwesomeIcons.Flag,
                    NavigationAction = async () =>
                        await _navigationService.PushNewNavigation<ImportantNoticeViewModel>(string.Empty)
                }

            };
        }

        private void NavigateMenu(AppMenuItem obj)
        {
            obj.NavigationAction.Invoke();
            AppMenus.ForEach(x => x.IsSelected = false);
            obj.IsSelected = true;
        }

        public override async Task Initialize(object navigationData)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await ErrorMethod();
            }
            else
            {
                var index = 0;
                if (!_settingsService.FillSettingsDetails())
                {
                    if (await _alertMessageService.ShowAlertForConfirmation("Confirm",
                        "You have not set your timeline. Do you want to save it to get notified.", "Yes", "No"))
                    {
                        index = 3;
                        DoMenuSelectionWork(index, navigationData);
                    }
                    else
                    {
                        DoMenuSelectionWork(index,navigationData);
                    }
                }
                else
                {
                    DoMenuSelectionWork(index,navigationData);
                }               
            }
        }

        private void DoMenuSelectionWork( int index, object navigationData)
        {
            if (navigationData != null)
            {
                var item = AppMenus.FirstOrDefault(x => x.MenuName == navigationData.ToString());
                if (item != null)
                    index = AppMenus.IndexOf(item);
            }
            AppMenus.ForEach(x => x.IsSelected = false);
            SelectedMenu = AppMenus[index];
            SelectedMenu.IsSelected = true;
            SelectedMenu.NavigationAction.Invoke();
        }
        private async Task ErrorMethod()
        {
            var errorData = new ErrorDetails()
            {
                ErrorMessage = "Internet is not available",
                NavigationBackAction = async (parameter) =>
                {
                    await _navigationService.NavigateToAsync<MainViewModel>(parameter);
                },
                NavigationData = string.Empty
            };
            await _navigationService.NavigateToAsync<ErrorViewModel>(errorData);
        }
    }
}
