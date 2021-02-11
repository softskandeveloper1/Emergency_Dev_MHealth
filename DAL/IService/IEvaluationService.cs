using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IEvaluationService
    {
        int AddPediatricEvaluation(mp_pediatric_evaluation evaluation);
        int AddPedEvaluationHistory(mp_ped_evaluation_history evaluation_History);
        void AddSymptoms(List<mp_ped_symptomps> symptoms);
        int AddHealthHistory(mp_phc_health_history health);
        int AddMedicaHistory(mp_phc_medical_history medical);
        int AddMentalStatus(mp_phc_mental_status mental);
        int AddSocialHistory(mp_phc_social_history social);
        int AddSystemReview(mp_phc_system_review system);
        int AddChildScreening(mp_child_screening screening);

        IQueryable<mp_pediatric_evaluation> GetPediatricEvaluation();
        IQueryable<mp_ped_evaluation_history> GetPedEvaluationHistory();
        IQueryable<mp_ped_symptomps> GetSymptoms();
        IQueryable<mp_phc_health_history> GetHealthHistory();
        IQueryable<mp_phc_medical_history> GetMedicalHistory();
        IQueryable<mp_phc_mental_status> GetMentalStatus();
        IQueryable<mp_phc_social_history> GetSocialHistory();
        IQueryable<mp_phc_system_review> GetSystemReview();
        IQueryable<mp_child_screening> GetChildScreening();
    }
}
