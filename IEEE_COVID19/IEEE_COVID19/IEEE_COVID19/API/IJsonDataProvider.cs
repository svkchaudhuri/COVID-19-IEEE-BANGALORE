using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IEEE_COVID19.API
{
    public interface IJsonDataProvider
    {
        Task<WorldCase> GetWorldData();
        Task<IndianCases> GetIndianData();
        Task<List<StateCases>> GetKarnatakaData();
    }
}
