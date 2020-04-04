using System;
using System.Collections.Generic;
using System.Text;

namespace IEEE_COVID19.API
{
   public class StateDataResponse
    {
        public List<Statewise> statewise { get; set; }
    }


   public class Delta
   {
       public int active { get; set; }
       public int confirmed { get; set; }
       public int deaths { get; set; }
       public int recovered { get; set; }
   }

   public class Statewise
   {
       public int active { get; set; }
       public int confirmed { get; set; }
       public int deaths { get; set; }
       public Delta delta { get; set; }
       public string lastupdatedtime { get; set; }
       public int recovered { get; set; }
       public string state { get; set; }
   }
  
}
