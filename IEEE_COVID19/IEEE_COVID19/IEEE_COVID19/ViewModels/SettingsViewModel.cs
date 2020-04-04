using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using IEEE_COVID19.Models;
using IEEE_COVID19.Notification;
using IEEE_COVID19.Services.MessageService;
using IEEE_COVID19.Services.SettingsService;
using IEEE_COVID19.ViewModels.Base;
using Xamarin.Forms;


namespace IEEE_COVID19.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IAlertMessageService _alertMessageService;
        private bool _isSettingsSaved;
        private string _contact1;
        private string _contact2;
        private TimeSpan _breakFasTimeSpan;
        private TimeSpan _lunchTimeSpan;
        private TimeSpan _dinnerTimeSpan;
        private TimeSpan _sleepTimeSpan;
        public string Contact1
        {
            get => _contact1;
            set
            {
                _contact1 = value;
                RaisePropertyChanged(() => Contact1);
            }
        }
        public string Contact2
        {
            get => _contact2;
            set
            {
                _contact2 = value;
                RaisePropertyChanged(() => Contact2);
            }
        }
        public TimeSpan BreakFast
        {
            get => _breakFasTimeSpan;
            set { _breakFasTimeSpan = value; RaisePropertyChanged(() => BreakFast); }
        }
        public TimeSpan LunchTime
        {
            get => _lunchTimeSpan;
            set { _lunchTimeSpan = value; RaisePropertyChanged(() => LunchTime); }
        }
        public TimeSpan DinnerTime
        {
            get => _dinnerTimeSpan;
            set { _dinnerTimeSpan = value; RaisePropertyChanged(() => DinnerTime); }
        }
        public TimeSpan SleepTime
        {
            get => _sleepTimeSpan;
            set { _sleepTimeSpan = value; RaisePropertyChanged(() => SleepTime); }
        }
        public bool IsSettingSaved
        {
            get => _isSettingsSaved;
            set { _isSettingsSaved = value; RaisePropertyChanged(() => IsSettingSaved); }
        }
        public SettingsViewModel(ISettingsService settingsService, IAlertMessageService alertMessageService)
        {
            _settingsService = settingsService;
            _alertMessageService = alertMessageService;
            SaveSettingsCommand = new Command(SaveSettings);
        }

        private async void SaveSettings(object obj)
        {
            if (!await IsParameterValid())
                return;
            var applicationSettings = new ApplicationSettings()
            {
                Contact1 = Contact1,
                Contact2 = Contact2,
                BreakFast = BreakFast,
                LunchTime = LunchTime,
                DinnerTime = DinnerTime,
                SleepTime = SleepTime
            };
            await _settingsService.SaveSettingsDetails(applicationSettings);
            await _alertMessageService.ShowAlert("Alert", "Settings saved.");               
            AlarmSetup(GetNearestTime());
        }

        private async Task<bool> IsParameterValid()
        {
            bool isValid = true;
            var message = string.Empty;
            var timeList = new List<TimeSpan>
            {
                BreakFast, LunchTime, DinnerTime, SleepTime
            };
            if (string.IsNullOrEmpty(Contact1) || Contact1.Length != 10)
            {
                message = "Invalid first contact\n";
                isValid = false;
            }

            if (!string.IsNullOrEmpty(Contact2) && Contact2.Length != 10)
            {
                message = message + "Invalid second contact\n";
                isValid = false;
            }

            if (timeList.Select(x => x.TotalMinutes).Distinct().Count() != timeList.Count)
            {
                message = message + "Two schedule time can not be same";
                isValid = false;
            }
            if (!isValid)
                await _alertMessageService.ShowAlert("Error", message);
            return isValid;
        }

        public ICommand SaveSettingsCommand { get; }

        public override async Task Initialize(object navigationData)
        {
            
            IsSettingSaved = _settingsService.FillSettingsDetails();            
            if (IsSettingSaved)
            {
                Contact1 = _settingsService.ApplicationSettings.Contact1;
                Contact2 = _settingsService.ApplicationSettings.Contact2;
                BreakFast = _settingsService.ApplicationSettings.BreakFast;
                LunchTime = _settingsService.ApplicationSettings.LunchTime;
                DinnerTime = _settingsService.ApplicationSettings.DinnerTime;
                SleepTime = _settingsService.ApplicationSettings.SleepTime;
                AlarmSetup(GetNearestTime());                
            }            
        }
        
        private void AlarmSetup(TimeSpan time)
        {
            var depen = DependencyService.Get<IAlarmNotificationManager>();
            depen.SetReminder(time.Hours, time.Minutes, "Hand wash", "Did you wash your hand?");           
        }

        private TimeSpan GetNearestTime()
        {
            var list = new List<TimeSpan> { BreakFast, LunchTime, DinnerTime, SleepTime };
            var dateTime = DateTime.Now;
            TimeSpan closestTimeSpan = list.Where(x => (x.Hours > dateTime.Hour) || (x.Hours == dateTime.Hour && x.Minutes > dateTime.Minute)).OrderBy(y=>y.Ticks).FirstOrDefault();
            if (closestTimeSpan == null || closestTimeSpan.Ticks == 0 )
                closestTimeSpan = list.Min();
            return closestTimeSpan;
        }
    }
}