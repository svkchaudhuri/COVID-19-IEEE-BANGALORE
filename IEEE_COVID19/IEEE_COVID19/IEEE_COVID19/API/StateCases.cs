using System;
using System.Collections.Generic;
using System.Text;

namespace IEEE_COVID19.API
{
    public class StateCases
    {
        public DateTime DayOf { get; set; }
        public string State { get; set; }
        public int TotalCases
        {
            get => (ActiveCases + RecoveredCases + Death);
        }
        public int ActiveCases { get; set; }
        public int RecoveredCases { get; set; }
        public int Death { get; set; }
        public int Critical { get; set; }

        public string ActiveChange { get; set; }
        public string RecoveredChange { get; set; }
        public string DeathChange { get; set; }

        public static implicit operator StateCases(List<StateCases> v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator List<object>(StateCases v)
        {
            throw new NotImplementedException();
        }
    }
}
