using System;
using System.Collections.Generic;
using System.Text;

namespace IEEE_COVID19.API
{
    public class WorldCase
    {
       public DateTime DayOf { get; set; }
       public int TotalCases
        {
            get => (ActiveCases + RecoveredCases + Death);
        }
        public int ActiveCases { get; set; }
        public int RecoveredCases { get; set; }
        public int Death { get; set; }
        public int Critical { get; set; }
    }
}
