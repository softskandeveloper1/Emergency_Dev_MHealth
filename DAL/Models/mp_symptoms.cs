using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_symptoms
    {
        public mp_symptoms()
        {
            mp_ped_symptomps = new HashSet<mp_ped_symptomps>();
        }

        public int id { get; set; }
        public string symptom { get; set; }

        public virtual ICollection<mp_ped_symptomps> mp_ped_symptomps { get; set; }
    }
}
