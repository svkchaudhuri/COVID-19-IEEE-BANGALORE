using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform;
using System.Threading.Tasks;
using IEEE_COVID19.ViewModels.Base;
using IEEE_COVID19.API;
using System.Linq;
using System.Windows.Input;
using IEEE_COVID19.Notification;
using IEEE_COVID19.Services.SettingsService;
using IEEE_COVID19.Services.NavigationService;
using IEEE_COVID19.Models;

namespace IEEE_COVID19.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IJsonDataProvider _jsonDataProvider;
        private string _name;
        private WorldCase _worldCase;
        private IndianCases _indianCases;
        private StateCases _stateCases;
        public List<StateCases> _lstStateCases;
        private bool _isHealthVisible;
        private ISettingsService _settingsSEervice;
        private INavigationService _navigationService;
        private int _percentage;
        public ICommand HealthFeelCmd { get; }
        public bool IsHealthVisible { get => _isHealthVisible; set { _isHealthVisible = value; RaisePropertyChanged(() => IsHealthVisible); } }
        public ICommand CmdCloseDetails { get; }
        public ICommand CmdShowWashDetails { get; }
        public CleanboardViewModel CleanboardViewModel { get; set; }

        public List<StateCases> LstStateCases
        {
            get => _lstStateCases; set
            {
                _lstStateCases = value; RaisePropertyChanged(() => LstStateCases);
            }
        }

        public string Name
        {
            get => _name;
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        public WorldCase WorldCase
        {
            get => _worldCase;
            set { _worldCase = value; RaisePropertyChanged(() => WorldCase); }
        }

        public IndianCases IndianCases
        {
            get => _indianCases;
            set { _indianCases = value; RaisePropertyChanged(() => IndianCases); }
        }
        public StateCases SelectedStateCases
        {
            get => _stateCases;
            set { _stateCases = value; RaisePropertyChanged(() => SelectedStateCases); }

        }

        public int Percentage
        {
            get => _percentage;
            private set { _percentage = value; RaisePropertyChanged(() => Percentage); }
        }

        public HomeViewModel(IJsonDataProvider jsonDataProvider, ISettingsService settingsService, INavigationService navigationService)
        {
            _jsonDataProvider = jsonDataProvider;
            _settingsSEervice = settingsService;
            _navigationService = navigationService;
            HealthFeelCmd = new Command(() => IsHealthVisible = true);
            CmdCloseDetails = new Command(() => IsHealthVisible = false);

            CleanboardViewModel = new CleanboardViewModel(_settingsSEervice);
            CleanboardViewModel.WashDetailsChanged += CleanboardViewModel_WashDetailsChanged;
            CmdShowWashDetails = new Command(() => CleanboardViewModel.IsVisible = true);
        }

        private void CleanboardViewModel_WashDetailsChanged(object sender, EventArgs e)
        {
            var score = _settingsSEervice.FillScoreDetail();
            Percentage = (int)score.Percentage;
        }

        public override async Task Initialize(object navigationData)
        {
            IsBusy = true;
            Name = "Home page";
            if (_settingsSEervice.FillSettingsDetails())
                AlarmSetup(GetNearestTime());
            var score = _settingsSEervice.FillScoreDetail();
            if (score.SaveDate.Date != DateTime.Now.Date)
            {
                var newScore = new ScoreDetail { PerformedCount = score.PerformedCount, SaveDate = DateTime.Now, TodaysPerformedCount = 0, TotalCount = score.TotalCount + 4 };
                await _settingsSEervice.SaveScoreDetails(newScore);
                Percentage = (int)score.Percentage;
            }
            else
            {
                Percentage = (int)score.Percentage;
            }
            WorldCase = await _jsonDataProvider.GetWorldData();
            IndianCases = await _jsonDataProvider.GetIndianData();

            LstStateCases = await _jsonDataProvider.GetKarnatakaData();
            SelectedStateCases = LstStateCases.First(x => x.State.StartsWith("Ka"));

            IsBusy = false;
        }

        private void AlarmSetup(TimeSpan time)
        {
            var depen = DependencyService.Get<IAlarmNotificationManager>();
            depen.AlarmNotificationReceived += AlarmItemClicked;
            depen.SetReminder(time.Hours, time.Minutes, "Hand wash", "Did you wash you hand?");
        }

        private void AlarmItemClicked(object sender, EventArgs e)
        {
            AlarmSetup(GetNearestTime());
            CleanboardViewModel.IsVisible = true;
        }

        private TimeSpan GetNearestTime()
        {
            var appSettings = _settingsSEervice.ApplicationSettings;
            var list = new List<TimeSpan> { appSettings.BreakFast, appSettings.LunchTime, appSettings.DinnerTime, appSettings.SleepTime };
            var dateTime = DateTime.Now;
            TimeSpan closestTimeSpan = list.Where(x => (x.Hours > dateTime.Hour) || (x.Hours == dateTime.Hour && x.Minutes > dateTime.Minute)).OrderBy(y => y.Ticks).FirstOrDefault();
            if (closestTimeSpan == null || closestTimeSpan.Ticks == 0)
                closestTimeSpan = list.Min();
            return closestTimeSpan;
        }

    }
}