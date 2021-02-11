using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_ped_symptomps
    {
        public int id { get; set; }
        public int symptom_id { get; set; }
        public string symptom_text_other { get; set; }
        public int ped_evaluation_id { get; set; }

        public virtual mp_pediatric_evaluation ped_evaluation_ { get; set; }
        public virtual mp_symptoms symptom_ { get; set; }
    }
}
