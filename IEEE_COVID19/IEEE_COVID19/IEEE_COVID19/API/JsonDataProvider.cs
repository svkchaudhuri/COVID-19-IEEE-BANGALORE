using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IEEE_COVID19.API
{
    public class JsonDataProvider : IJsonDataProvider
    {
        private static readonly HttpClient HttpClient;
        private static readonly HttpClient HttpClientState;
        static JsonDataProvider()
        {
            HttpClient = new HttpClient { BaseAddress = new Uri(@"https://corona-api.com/") };
            HttpClientState = new HttpClient {BaseAddress = new Uri(@"https://api.covid19india.org/")};
        }
        public async Task<WorldCase> GetWorldData()
        {
            var worldCase = new WorldCase();
            var response = await HttpClient.GetAsync(@"countries");
            if (!response.IsSuccessStatusCode) return worldCase;
            var result = await response.Content.ReadAsStringAsync();
            var allCases = JsonConvert.DeserializeObject<WorldDataResponse>(result);
            var date = allCases.data.Max(x => x.updated_at);
            var latestData = allCases.data.Select(x => x.latest_data).ToList();
            worldCase.ActiveCases = latestData.Sum(x => x.confirmed);
            worldCase.RecoveredCases = latestData.Sum(x => x.recovered);
            worldCase.Death = latestData.Sum(x => x.deaths);
            worldCase.Critical = latestData.Sum(x => x.critical);
            worldCase.DayOf = date;
            return worldCase;
        }

        public async Task<IndianCases> GetIndianData()
        {
            var response = await HttpClient.GetAsync(@"countries/IN");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var indianCase = JsonConvert.DeserializeObject<CountryDataResponse>(result);
                return GetIndianCases(indianCase);
            }
            throw new Exception("Unable to fetch data.");
        }

        public async Task<List<StateCases>> GetKarnatakaData()
        {
            var allStateData = new List<StateCases>();
            var response = await HttpClientState.GetAsync(@"data.json");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var stateCases = JsonConvert.DeserializeObject<StateDataResponse>(result);
                
                stateCases.statewise.ForEach(x =>
                {
                    if(x!=null)
                    {
                    var state = new StateCases()
                    {
                        ActiveCases = x.active,
                        Death = x.deaths,
                        RecoveredCases = x.recovered,
                        ActiveChange = SetIndividualState(x.delta!=null? x.delta.active:0),
                        RecoveredChange = SetIndividualState(x.delta!=null? x.delta.recovered:0),
                        DeathChange = SetIndividualState(x.delta!=null? x.delta.deaths:0),
                        State = x.state
                    };
                    allStateData.Add(state);
                    }
                });
                var total = allStateData.FirstOrDefault(x =>
                    x.State.Equals("Total", StringComparison.InvariantCultureIgnoreCase));
                if (total != null)
                    allStateData.Remove(total);
                return allStateData;
            }
            throw new Exception("Unable to fetch data.");
        }

        private IndianCases GetIndianCases(CountryDataResponse countryDataResponse)
        {
            var indianCases = new IndianCases
            {
                Death = countryDataResponse.data.latest_data.deaths,
                ActiveCases = countryDataResponse.data.latest_data.confirmed,
                Critical = countryDataResponse.data.latest_data.critical,
                RecoveredCases = countryDataResponse.data.latest_data.recovered
            };
           SetChangeData(indianCases, countryDataResponse.data.timeline);
            return indianCases;
        }

        private void SetChangeData(IndianCases indianCases, List<Timeline> timeLines)
        {
            var latestThreeData = timeLines.OrderByDescending(x => x.updated_at).Take(3).ToList();
            if (latestThreeData.Count == 3)
            {
                var activeSlope = (latestThreeData[2].active - latestThreeData[1].active) -
                                  (latestThreeData[1].active - latestThreeData[0].active);
               indianCases.ActiveChange = SetIndividualState(activeSlope);
                var deathSlope = (latestThreeData[2].deaths - latestThreeData[1].deaths) -
                                 (latestThreeData[1].deaths - latestThreeData[0].deaths);
                indianCases.DeathChange = SetIndividualState(deathSlope);
                var recoveredSlope = (latestThreeData[2].recovered - latestThreeData[1].recovered) -
                                 (latestThreeData[1].recovered - latestThreeData[0].recovered);
                indianCases.RecoveredChange = SetIndividualState( recoveredSlope);
            }
            else
            {
                indianCases.ActiveChange = "N";
                indianCases.DeathChange = "N";
                indianCases.RecoveredChange = "N";
            }
        }

        private string SetIndividualState( int count)
        {
            if (count == 0)
                return "N";
            else if (count > 0)
                return "U";
            return  "D";
        }
    }

   
}
