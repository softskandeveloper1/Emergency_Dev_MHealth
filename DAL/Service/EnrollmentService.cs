using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class EnrollmentService: IEnrollmentService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_enrollment enrollment)
        {
            enrollment.created_at = DateTime.Now;
            _context.mp_enrollment.Add(enrollment);
            _context.SaveChanges();
        }

        public IQueryable<mp_enrollment> Get()
        {
            return _context.mp_enrollment.AsQueryable();
        }

        public mp_enrollment Get(int id)
        {
            return _context.mp_enrollment.FirstOrDefault(e => e.id == id);
        }

        public mp_enrollment Get(Guid profile_id)
        {
            return _context.mp_enrollment.FirstOrDefault(e => e.profile_id == profile_id);
        }

        public void Update(mp_enrollment enrollment)
        {
            var old = _context.mp_enrollment.FirstOrDefault(e => e.id == enrollment.id);
            enrollment.created_at = old.created_at;
            enrollment.created_by = old.created_by;

            _context.Entry(old).CurrentValues.SetValues(enrollment);
            _context.SaveChanges();
        }
    }
}
