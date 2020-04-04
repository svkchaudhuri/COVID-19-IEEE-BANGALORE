using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IEEE_COVID19.Models;

namespace IEEE_COVID19.Services.SettingsService
{
    public interface ISettingsService
    {
        ApplicationSettings ApplicationSettings { get; set; }
        Task SaveSettingsDetails(ApplicationSettings applicationSettings);
        bool FillSettingsDetails();
        Task SaveScoreDetails(ScoreDetail scoreDetals);
        ScoreDetail FillScoreDetail();
    }
}
