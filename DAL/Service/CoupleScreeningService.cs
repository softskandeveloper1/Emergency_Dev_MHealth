using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class CoupleScreeningService: ICoupleScreeningService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_couple_screening screening)
        {
            screening.created_at = DateTime.Now;
            _context.mp_couple_screening.Add(screening);
            _context.SaveChanges();
        }

        public IQueryable<mp_couple_screening> Get()
        {
            return _context.mp_couple_screening.AsQueryable();
        }
    }
}
