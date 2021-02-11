using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ServiceCostService: IServiceCostService
    {
        private readonly HContext _context = new HContext();

        public void AddOrUpdate(mp_service_costing service_cost)
        {
            var old = _context.mp_service_costing.FirstOrDefault(e => e.clinician_id == service_cost.clinician_id && e.appointment_service_id==service_cost.appointment_service_id && e.appointment_activity_sub_id==service_cost.appointment_activity_sub_id);
            if (old != null)
            {
                service_cost.id = old.id;
                service_cost.created_at = old.created_at;
                service_cost.created_by = old.created_by;
                service_cost.updated_at = DateTime.Now;

                _context.Entry(old).CurrentValues.SetValues(service_cost);
            }
            else
            {
                service_cost.created_at = DateTime.Now;
                _context.mp_service_costing.Add(service_cost);
            }

            _context.SaveChanges();
        }

        public IQueryable<mp_service_costing> Get()
        {
            return _context.mp_service_costing.AsQueryable();
        }
    }
}
