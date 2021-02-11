using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_phc_mental_status
    {
        public int id { get; set; }
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
        public Guid profile_id { get; set; }
        public string create_by { get; set; }
        public DateTime? created_at { get; set; }
        public Guid? appointment_id { get; set; }
    }
}
