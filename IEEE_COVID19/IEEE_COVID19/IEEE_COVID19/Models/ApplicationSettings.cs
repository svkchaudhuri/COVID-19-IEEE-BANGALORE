using System;
using System.Collections.Generic;
using System.Text;

namespace IEEE_COVID19.Models
{
    public class ApplicationSettings
    {
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public TimeSpan BreakFast { get; set; }
        public TimeSpan LunchTime { get; set; }
        public TimeSpan DinnerTime { get; set; }
        public TimeSpan SleepTime { get; set; }
    }
}
