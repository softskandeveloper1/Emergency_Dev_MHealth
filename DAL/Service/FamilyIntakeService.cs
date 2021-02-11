using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class FamilyIntakeService: IFamilyIntakeService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_family_intake intake)
        {
            intake.created_at = DateTime.Now;
            _context.mp_family_intake.Add(intake);
            _context.SaveChanges();
        }

        public IQueryable<mp_family_intake> GetClientIntkakes(Guid client_id)
        {
            return _context.mp_family_intake.Where(e => e.profile_id == client_id);
        }

        public mp_family_intake Get(int id)
        {
            return _context.mp_family_intake.FirstOrDefault(e => e.id == id);
        }

        public IQueryable<mp_family_intake> Get()
        {
            return _context.mp_family_intake.AsQueryable();
        }

        public void Update(mp_family_intake intake)
        {
            var old = _context.mp_family_intake.FirstOrDefault(e => e.id == intake.id);
            intake.updated_at = DateTime.Now;

            intake.created_at = old.created_at;
            intake.created_by = old.created_by;

            _context.Entry(old).CurrentValues.SetValues(intake);
            _context.SaveChanges();
        }
    }
}
