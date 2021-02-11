using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class CostingModel
    {
        public string sub_name { set; get; }
        public int sub_id { set; get; }
        public string service_name { set; get; }
        public int service_id { set; get; }
        public decimal cost { set; get; }
    }
}
