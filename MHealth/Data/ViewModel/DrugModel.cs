using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class DrugModel
    {
        public DrugModel()
        {

        }

        public DrugModel(mp_prescription_drug p_drug)
        {
            id = p_drug.id;
            prescription_id = p_drug.prescription_id;
            dosage = p_drug.dosage;
            drug = p_drug.drug;
        }
        public long id { get; set; }
        public long prescription_id { get; set; }
        public string dosage { get; set; }
        public string drug { get; set; }
    }
}
