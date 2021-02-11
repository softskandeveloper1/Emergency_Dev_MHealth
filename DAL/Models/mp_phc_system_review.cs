using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_phc_system_review
    {
        public int id { get; set; }
        public int? fatigue_weight_loss { get; set; }
        public int? fatigue_fever { get; set; }
        public int? fatigue_chills { get; set; }
        public int? fatigue_night_sweat { get; set; }
        public int? cv_chest_pain { get; set; }
        public int? cv_edema { get; set; }
        public int? cv_pnd { get; set; }
        public int? cv_orthopnea { get; set; }
        public int? cv_palpitations { get; set; }
        public int? cv_claudication { get; set; }
        public int? gu_dysuria { get; set; }
        public int? gu_frequency { get; set; }
        public int? gu_hematuria { get; set; }
        public int? gu_discharge { get; set; }
        public int? gu_mensutral { get; set; }
        public int? endo_polyuria { get; set; }
        public int? endo_polydipsia { get; set; }
        public int? endo_polyphagia { get; set; }
        public int? endo_heat_cold { get; set; }
        public int? endo_pruritis { get; set; }
        public int? eyes_visual_change { get; set; }
        public int? eyes_pain { get; set; }
        public int? eyes_redness { get; set; }
        public int? respiratory_cough { get; set; }
        public int? respiratory_wheezing { get; set; }
        public int? respiratory_hypersomnolence { get; set; }
        public int? respiratory_night_sweat { get; set; }
        public int? musc_arthritis { get; set; }
        public int? musc_joint_swelling { get; set; }
        public int? musc_myalgias { get; set; }
        public int? musc_backpain { get; set; }
        public int? neuro_weaken { get; set; }
        public int? neuro_seizures { get; set; }
        public int? neuro_paresthesia { get; set; }
        public int? neuro_tremor { get; set; }
        public int? neuro_syncope { get; set; }
        public int? ent_headaches { get; set; }
        public int? ent_hoarseness { get; set; }
        public int? ent_sore_throat { get; set; }
        public int? ent_epistasis { get; set; }
        public int? ent_sinus { get; set; }
        public int? ent_hearing_loss { get; set; }
        public int? ent_tinnitus { get; set; }
        public int? gl_adb_pain { get; set; }
        public int? gl_nausea { get; set; }
        public int? gl_diarrhea { get; set; }
        public int? gl_heartburn { get; set; }
        public int? gl_blood_stool { get; set; }
        public int? heme_bleeding { get; set; }
        public int? heme_bruising { get; set; }
        public int? heme_clotting { get; set; }
        public int? heme_lymph { get; set; }
        public int? heme_night_sweat { get; set; }
        public Guid profile_id { get; set; }
        public string created_by { get; set; }
        public DateTime? created_at { get; set; }
    }
}
