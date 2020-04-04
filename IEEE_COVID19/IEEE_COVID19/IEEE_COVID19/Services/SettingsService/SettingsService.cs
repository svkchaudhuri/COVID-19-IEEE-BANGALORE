using System;
using System.Threading.Tasks;
using IEEE_COVID19.Models;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace IEEE_COVID19.Services.SettingsService
{
    public class SettingsService : ISettingsService
    {
        private const string Settings = "Settings";
        private const string Score = "Score";
        public ApplicationSettings ApplicationSettings { get; set; }
        public bool FillSettingsDetails()
        {           
            if (!Application.Current.Properties.ContainsKey(Settings))
                return false;
            try
            {
                var savedSettings = (string)Application.Current.Properties[Settings];
                var applicationSettings = JsonConvert.DeserializeObject<ApplicationSettings>(savedSettings);
                ApplicationSettings = new ApplicationSettings()
                {
                    Contact1 = applicationSettings.Contact1,
                    Contact2 = applicationSettings.Contact2,
                    BreakFast = applicationSettings.BreakFast,
                    LunchTime = applicationSettings.LunchTime,
                    DinnerTime = applicationSettings.DinnerTime,
                    SleepTime = applicationSettings.SleepTime
                };
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task SaveSettingsDetails(ApplicationSettings applicationSettings)
        {
            var convertedValue = JsonConvert.SerializeObject(applicationSettings);
            Application.Current.Properties[Settings] = convertedValue;
            await Application.Current.SavePropertiesAsync();
        }

        public async Task SaveScoreDetails(ScoreDetail scoreDetals)
        {
            var convertedValue = JsonConvert.SerializeObject(scoreDetals);
            Application.Current.Properties[Score] = convertedValue;
            await Application.Current.SavePropertiesAsync();
        }

        public ScoreDetail FillScoreDetail()
        {
            if (!Application.Current.Properties.ContainsKey(Score))
            {
                var scoreDetail = new ScoreDetail { PerformedCount = 0, TodaysPerformedCount = 0, SaveDate = DateTime.Now, TotalCount = 4 };
                return scoreDetail;
            }
            try
            {
                var savedSettings = (string)Application.Current.Properties[Score];
                var scoreSettings = JsonConvert.DeserializeObject<ScoreDetail>(savedSettings);
                return  new ScoreDetail()
                {
                    PerformedCount = scoreSettings.PerformedCount,
                   SaveDate = scoreSettings.SaveDate,
                   TotalCount = scoreSettings.TotalCount
                };                
            }
            catch (Exception ex)
            {
                return null;
            }
        }             
    }
}
