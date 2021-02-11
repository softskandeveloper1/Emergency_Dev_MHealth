using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_phc_health_history
    {
        public int id { get; set; }
        public int? head_brain_injury { get; set; }
        public int? weight_loss { get; set; }
        public int? seizures { get; set; }
        public int? stroke { get; set; }
        public int? eye_problems { get; set; }
        public int? missing_use_of_arm { get; set; }
        public int? hearing_problem { get; set; }
        public int? heart_disease { get; set; }
        public int? tobacco_use { get; set; }
        public int? fainting { get; set; }
        public int? failed_drug_test { get; set; }
        public int? uti { get; set; }
        public int? fibrillation { get; set; }
        public int? cirrhosis { get; set; }
        public int? copd { get; set; }
        public int? diabetes_meletus { get; set; }
        public int? hyperlipidemia { get; set; }
        public int? hypothyroidism { get; set; }
        public int? oa_djd { get; set; }
        public int? parkinson_disease { get; set; }
        public int? bone_problems { get; set; }
        public int? heart_procedures { get; set; }
        public int? bleeding_problems { get; set; }
        public int? chronic_disease { get; set; }
        public int? hpb { get; set; }
        public int? high_cholesterol { get; set; }
        public int? cancer { get; set; }
        public int? sleep_disorders { get; set; }
        public int? mental_health_problems { get; set; }
        public int? illegal_drug_use { get; set; }
        public int? neck_problems { get; set; }
        public int? cad { get; set; }
        public int? ckd { get; set; }
        public int? cvs_tia { get; set; }
        public int? gerd { get; set; }
        public int? hypertention { get; set; }
        public int? incontinence { get; set; }
        public int? obesity { get; set; }
        public int? prostate { get; set; }
        public int? sleep_test { get; set; }
        public int? kidney_probems { get; set; }
        public int? spent_a_night_in_hospital { get; set; }
        public int? chronic_cough { get; set; }
        public int? digestive_problems { get; set; }
        public int? had_broken_bone { get; set; }
        public int? lung_disease { get; set; }
        public int? sugar_problems { get; set; }
        public int? drink_alchohol { get; set; }
        public int? memory_loss { get; set; }
        public int? asthma { get; set; }
        public int? chf { get; set; }
        public int? constipation { get; set; }
        public int? dementia { get; set; }
        public int? gout { get; set; }
        public int? hyperthyroidism { get; set; }
        public int? multiple_selerosis { get; set; }
        public int? osteoporosis { get; set; }
        public Guid profile_id { get; set; }
        public string created_by { get; set; }
        public DateTime? created_at { get; set; }
        public Guid appointment_id { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
    }
}
