using IEEE_COVID19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IEEE_COVID19.ViewModels
{
    public class NewsDetailViewModel: ViewModelBase
    {
        private string _uri;
        public string URI { get {return _uri; } set { _uri = value; RaisePropertyChanged(() => URI); } }

        public override async Task Initialize(object navigationData)
        {
            await Task.Delay(1);
            URI = navigationData as string;
        }
    }
}
