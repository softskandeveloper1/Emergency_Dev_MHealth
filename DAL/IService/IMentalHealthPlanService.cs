using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IMentalHealthPlanService
    {
        long AddPlan(mp_mental_health_plan plan);
        void AddObjective(mp_mental_health_plan_objective objective);
        void AddReviewPeriod(mp_mental_health_plan_review_period period);
        mp_mental_health_plan Get(long id);
        IQueryable<mp_mental_health_plan> GetClientPlans(Guid client_id);
        IQueryable<mp_mental_health_plan> Get();
    }
}
