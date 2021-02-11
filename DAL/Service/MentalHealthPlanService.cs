using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class MentalHealthPlanService: IMentalHealthPlanService
    {
        private readonly HContext _context = new HContext();

        public long AddPlan(mp_mental_health_plan plan)
        {
            plan.created_at = DateTime.Now;
            _context.mp_mental_health_plan.Add(plan);
            _context.SaveChanges();

            return plan.id;
        }

        public void AddObjective(mp_mental_health_plan_objective objective)
        {
            objective.created_at = DateTime.Now;
            _context.mp_mental_health_plan_objective.Add(objective);
            _context.SaveChanges();
        }

        public void AddReviewPeriod(mp_mental_health_plan_review_period period)
        {
            _context.mp_mental_health_plan_review_period.Add(period);
            _context.SaveChanges();
        }

        public mp_mental_health_plan Get(long id)
        {
            return _context.mp_mental_health_plan.FirstOrDefault(e=>e.id==id);
        }

        public IQueryable<mp_mental_health_plan> GetClientPlans(Guid client_id)
        {
            return _context.mp_mental_health_plan.Where(e => e.profile_id == client_id)
                .Include(e => e.mp_mental_health_plan_objective)
                .Include(e => e.mp_mental_health_plan_review_period);
        }

        public IQueryable<mp_mental_health_plan> Get()
        {
            return _context.mp_mental_health_plan
                .Include(e => e.mp_mental_health_plan_objective)
                .Include(e => e.mp_mental_health_plan_review_period);
        }
    }
}
