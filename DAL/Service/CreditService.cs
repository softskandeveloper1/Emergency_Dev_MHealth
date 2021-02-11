using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class CreditService : ICreditService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_credit credit)
        {
            credit.created_at = DateTime.Now;
            _context.mp_credit.Add(credit);
            _context.SaveChanges();

        }

        public IQueryable<mp_credit> Get()
        {
            return _context.mp_credit.AsQueryable();
        }
    }
}
