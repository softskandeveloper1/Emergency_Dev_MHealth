using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class PharmacyService : IPharmacyService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_pharmacy pharmacy)
        {
            _context.mp_pharmacy.Add(pharmacy);
            _context.SaveChanges();
        }

        public IQueryable<mp_pharmacy> Get()
        {
            return _context.mp_pharmacy.AsQueryable();
        }

        public mp_pharmacy GetById(int id)
        {
            return _context.mp_pharmacy.FirstOrDefault(x => x.id == id);
        }
    }
}
