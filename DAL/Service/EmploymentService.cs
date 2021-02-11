using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class EmploymentService: IEmploymentService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_employment employment)
        {
            employment.created_at = DateTime.Now;
            _context.mp_employment.Add(employment);
            _context.SaveChanges();
        }

        public IQueryable<mp_employment> Get()
        {
            return _context.mp_employment.AsQueryable();
        }

        public mp_employment Get(int id)
        {
            return _context.mp_employment.FirstOrDefault(e => e.id == id);
        }

        public void Update(mp_employment employment)
        {
            var old = _context.mp_employment.FirstOrDefault(e => e.id == employment.id);
            employment.created_at = old.created_at;

            _context.Entry(old).CurrentValues.SetValues(employment);
            _context.SaveChanges();
        }
    }
}
