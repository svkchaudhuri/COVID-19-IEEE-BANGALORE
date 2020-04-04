using System;
using System.Collections.Generic;
using System.Text;

namespace IEEE_COVID19.Models
{
    public class ScoreDetail
    {
        public DateTime SaveDate { get; set; }
        public double TotalCount { get; set; }
        public double PerformedCount { get; set; }
        public double TodaysPerformedCount { get; set; }
        public double Percentage { get => (PerformedCount * 100) / TotalCount; }
    }
}
