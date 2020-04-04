using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IEEE_COVID19.Models;
using IEEE_COVID19.Services.SettingsService;
using IEEE_COVID19.ViewModels.Base;
using Xamarin.Forms;

namespace IEEE_COVID19.ViewModels
{
    public class CleanboardViewModel : ViewModelBase
    {
        private bool _isBreakfastHandWashed;
        private bool _isLunchHandWashed;
        private bool _isDinnerHandWashed;
        private bool _isBedTimeHandWashed;
        private bool _isVisible;
        private ISettingsService _settingsService;
        public event EventHandler WashDetailsChanged;
        public bool IsVisible
        {
            get => _isVisible; set
            {
                _isVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }
        public bool IsBreakfastHandWashed
        {
            get => _isBreakfastHandWashed; set
            {
                _isBreakfastHandWashed = value;
                RaisePropertyChanged(() => IsBreakfastHandWashed);
            }
        }
        public bool IsLunchHandWashed
        {
            get => _isLunchHandWashed; set
            {
                _isLunchHandWashed = value;
                RaisePropertyChanged(() => IsLunchHandWashed);
            }
        }
        public bool IsDinnerHandWashed
        {
            get => _isDinnerHandWashed; set
            {
                _isDinnerHandWashed = value;
                RaisePropertyChanged(() => IsDinnerHandWashed);
            }
        }
        public bool IsBedTimeHandWashed
        {
            get => _isBedTimeHandWashed; set
            {
                _isBedTimeHandWashed = value;
                RaisePropertyChanged(() => IsBedTimeHandWashed);
            }
        }

        public ICommand CmdSaveWashDetails { get; }
        public ICommand CmdHideDetails { get; }
        public ICommand CmdCloseDetails { get; }

        public CleanboardViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            CmdSaveWashDetails = new Command(SaveChanges);
            CmdHideDetails = new Command(ClearDetails);
            CmdCloseDetails = new Command(() => IsVisible = false);
        }

        private void SaveChanges(object obj)
        {
            var score = _settingsService.FillScoreDetail();
            double previousPerformedCount = 0;
            double increasedTotalCount = 0;
            if (score.SaveDate.Date == DateTime.Now.Date)
            {
                previousPerformedCount = score.PerformedCount - score.TodaysPerformedCount;
                increasedTotalCount = 4;
            }
            int count = 0;
            if (IsBreakfastHandWashed)
                count++;
            if (IsLunchHandWashed)
                count++;
            if (IsDinnerHandWashed)
                count++;
            if (IsBedTimeHandWashed)
                count++;
            var scoreDetails = new ScoreDetail
            {
                TotalCount = score.TotalCount - increasedTotalCount + 4,
                PerformedCount = score.PerformedCount - previousPerformedCount + count,
                SaveDate = DateTime.Now,
                TodaysPerformedCount = count
            };
            _settingsService.SaveScoreDetails(scoreDetails);
            IsVisible = false;
            WashDetailsChanged?.Invoke(this, new EventArgs());
        }

        public override async Task Initialize(object navigationData)
        {
            await Task.Delay(1);
        }

        private void ClearDetails()
        {
            IsBreakfastHandWashed = false;
            IsLunchHandWashed = false;
            IsDinnerHandWashed = false;
            IsBedTimeHandWashed = false;
        }
    }
}