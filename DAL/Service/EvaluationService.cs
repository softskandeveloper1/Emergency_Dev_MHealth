using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class EvaluationService: IEvaluationService
    {
        private readonly HContext _context = new HContext();

        public int AddPediatricEvaluation(mp_pediatric_evaluation evaluation)
        {
            var old = _context.mp_pediatric_evaluation.FirstOrDefault(e => e.appointment_id == evaluation.appointment_id && e.profile_id == evaluation.profile_id);
            if (old == null)
            {
                _context.Add(evaluation);
                
            }
            else
            {
                evaluation.created_by = old.created_by;
                evaluation.created_date = old.created_date;
                evaluation.id = old.id;

                _context.Entry(old).CurrentValues.SetValues(evaluation);
            }
            _context.SaveChanges();

            return evaluation.id;
        }

        public IQueryable<mp_pediatric_evaluation> GetPediatricEvaluation()
        {
            return _context.mp_pediatric_evaluation.AsQueryable();
        }

        public int AddPedEvaluationHistory(mp_ped_evaluation_history evaluation_History)
        {
            var history = _context.mp_ped_evaluation_history.FirstOrDefault(e => e.appointment_id == evaluation_History.appointment_id);
            if ( history== null)
            {
                _context.Add(evaluation_History);
                _context.SaveChanges();
                return evaluation_History.id;
            }
            else
            {
                return history.id;
            }

          
        }

        public IQueryable<mp_ped_evaluation_history> GetPedEvaluationHistory()
        {
            return _context.mp_ped_evaluation_history.AsQueryable();
        }

        public void AddSymptoms(List<mp_ped_symptomps> symptoms)
        {
            var eval_id = symptoms.FirstOrDefault().ped_evaluation_id;
            //check if symptons exists
            var symptns = _context.mp_ped_symptomps.Where(e => e.ped_evaluation_id == eval_id);
            if (symptoms.Any())
            {
                _context.mp_ped_symptomps.RemoveRange(symptns);
                _context.SaveChanges();
            }

            _context.AddRange(symptoms);
            _context.SaveChanges();
        }

        public IQueryable<mp_ped_symptomps> GetSymptoms()
        {
            return _context.mp_ped_symptomps.AsQueryable();
        }

        public int AddHealthHistory(mp_phc_health_history health)
        {
            var old = _context.mp_phc_health_history.FirstOrDefault(e => e.profile_id == health.profile_id && e.appointment_id == health.appointment_id);
            if (old == null)
            {
                _context.Add(health);
            }
            else
            {
                health.id = old.id;
                health.created_at = old.created_at;
                health.created_by = old.created_by;
                _context.Entry(old).CurrentValues.SetValues(health);
            }
           
            return _context.SaveChanges();
        }

        public IQueryable<mp_phc_health_history> GetHealthHistory()
        {
            return _context.mp_phc_health_history.AsQueryable();
        }

        public int AddMedicaHistory(mp_phc_medical_history medical)
        {
            _context.Add(medical);
            return _context.SaveChanges();
        }

        public IQueryable<mp_phc_medical_history> GetMedicalHistory()
        {
            return _context.mp_phc_medical_history.AsQueryable();
        }

        public int AddMentalStatus(mp_phc_mental_status mental)
        {
            var old = _context.mp_phc_mental_status.FirstOrDefault(e => e.profile_id == mental.profile_id && e.appointment_id == mental.appointment_id);
            if (old == null)
            {
                _context.Add(mental);
            }
            else
            {
                mental.id = old.id;
                mental.created_at = old.created_at;
                mental.create_by = old.create_by;

                _context.Entry(old).CurrentValues.SetValues(mental);
            }
            
            return _context.SaveChanges();
        }

        public IQueryable<mp_phc_mental_status> GetMentalStatus()
        {
            return _context.mp_phc_mental_status.AsQueryable();
        }

        public int AddSocialHistory(mp_phc_social_history social)
        {
            var old = _context.mp_phc_social_history.FirstOrDefault(e => e.appointment_id == social.appointment_id && e.profile_id == social.profile_id);
            if (old == null)
            {
                _context.Add(social);
            }
            else
            {
                social.id = old.id;
                social.created_at = old.created_at;
                social.created_by = old.created_by;

                _context.Entry(old).CurrentValues.SetValues(social);
            }
           
            return _context.SaveChanges();
        }

        public IQueryable<mp_phc_social_history> GetSocialHistory()
        {
            return _context.mp_phc_social_history.AsQueryable();
        }

        public int AddSystemReview(mp_phc_system_review system)
        {
            _context.Add(system);
            return _context.SaveChanges();
        }

        public IQueryable<mp_phc_system_review> GetSystemReview()
        {
            return _context.mp_phc_system_review.AsQueryable();
        }

        public int AddChildScreening(mp_child_screening screening)
        {
            _context.Add(screening);
            return _context.SaveChanges();
        }

        public IQueryable<mp_child_screening> GetChildScreening()
        {
            return _context.mp_child_screening.AsQueryable();
        }
    }
}
