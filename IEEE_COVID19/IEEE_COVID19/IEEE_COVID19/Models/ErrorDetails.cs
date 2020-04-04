using System;
using System.Collections.Generic;
using System.Text;

namespace IEEE_COVID19.Models
{
    public class ErrorDetails
    {
        public string ErrorMessage { get; set; }
        public Action<object> NavigationBackAction { get; set; }
        public object NavigationData { get; set; }
    }
}
