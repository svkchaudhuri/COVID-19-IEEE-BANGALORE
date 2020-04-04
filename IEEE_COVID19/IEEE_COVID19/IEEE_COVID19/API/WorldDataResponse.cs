using System;
using System.Collections.Generic;
using System.Text;

namespace IEEE_COVID19.API
{
    public class WorldDataResponse
    {
        public List<Country> data { get; set; }
    }

    public class CountryDataResponse
    {
        public CountryWithTimeLine data { get; set; }
    }
    public class CountryWithTimeLine: Country
    {
        public List<Timeline> timeline { get; set; }
    }

    public class Coordinates
    {
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }

    public class Today
    {
        public int deaths { get; set; }
        public int confirmed { get; set; }
    }

    public class Analysis
    {
        public double? death_rate { get; set; }
        public double? recovery_rate { get; set; }
        public object recovered_vs_death_ratio { get; set; }
        public double cases_per_million_population { get; set; }
    }

    public class PatientCount
    {
        public int deaths { get; set; }
        public int confirmed { get; set; }
        public int recovered { get; set; }
        public int critical { get; set; }
        public Analysis calculated { get; set; }
    }

    public class Country
    {
        public Coordinates coordinates { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int? population { get; set; }
        public DateTime updated_at { get; set; }
        public Today today { get; set; }
        public PatientCount latest_data { get; set; }
    }

    public class Timeline
    {
        public DateTime updated_at { get; set; }
        public string date { get; set; }
        public int deaths { get; set; }
        public int confirmed { get; set; }
        public int active { get; set; }
        public int recovered { get; set; }
        public int new_confirmed { get; set; }
        public int new_recovered { get; set; }
        public int new_deaths { get; set; }
        public bool is_in_progress { get; set; }
    }
}
