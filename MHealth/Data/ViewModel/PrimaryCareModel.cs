using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class PrimaryCareModel
    {
        public PrimaryCareModel()
        {

        }

        public PrimaryCareModel(mp_phc_health_history health_History, mp_phc_mental_status mental_Status, mp_phc_social_history social_History)
        {

        }

        //mp_phc_health_history


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


        //mp_phc_mental_status

        public string dressing_grooming { get; set; }
        public string facial_expression { get; set; }
        public string eye_contacts { get; set; }
        public string dressing_grooming_comment { get; set; }
        public string facial_expression_comment { get; set; }
        public string eye_contacts_comment { get; set; }
        public string motor_activity { get; set; }
        public string anxiety_level { get; set; }
        public string agitation_level { get; set; }
        public string motor_activity_comment { get; set; }
        public string anxiety_level_comment { get; set; }
        public string agitation_level_comment { get; set; }
        public string production_of_speech { get; set; }
        public string rate_of_speech { get; set; }
        public string volume { get; set; }
        public string production_of_speech_comment { get; set; }
        public string rate_of_speech_comment { get; set; }
        public string volume_comment { get; set; }
        public string mood { get; set; }
        public string affect { get; set; }
        public string mood_comment { get; set; }
        public string affect_comment { get; set; }
        public string flow_of_thought { get; set; }
        public string perceptual_function { get; set; }
        public string hallucinations { get; set; }
        public string flow_of_thought_comment { get; set; }
        public string perceptual_function_comment { get; set; }
        public string hallucinations_comment { get; set; }
        public string delusion { get; set; }
        public string thought_content { get; set; }
        public string suicidal_behaviour { get; set; }
        public string depression { get; set; }
        public string delusion_comment { get; set; }
        public string thought_content_comment { get; set; }
        public string suicidal_behaviour_comment { get; set; }
        public string depression_comment { get; set; }
        public string appetite { get; set; }
        public string sleep { get; set; }
        public string elimination { get; set; }
        public string commons { get; set; }
        public string sleep_comment { get; set; }
        public string elimination_comment { get; set; }
        public string state_of_conciousness { get; set; }
        public string sensorium { get; set; }
        public string memory { get; set; }
        public string judgement_insight { get; set; }
        public string state_of_conciousness_comment { get; set; }
        public string sensorium_comment { get; set; }
        public string memory_comment { get; set; }
        public string judgement_insight_comment { get; set; }
        public string impulse_control { get; set; }
        public string insight { get; set; }
        public string judgement { get; set; }
        public string interview_reaction { get; set; }
        public string insight_comment { get; set; }
        public string judgement_comment { get; set; }
        public string interview_reaction_comment { get; set; }

        //mp_phc_social_history

        public int? marital_status { get; set; }
        public string smoking { get; set; }
        public string alchohol { get; set; }
        public int? past_drug_use { get; set; }
        public int? current_drug_use { get; set; }

    }
}
