using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class ServiceCostModel
    {
        public Guid clinician_id { set; get; }
        public string created_by { set; get; }

        public List<CostingModel> CostingModels { set; get; }
    }
}
