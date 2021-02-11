using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ReferralService: IReferralService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_referral referral)
        {
           
            referral.created_at = DateTime.Now;
            _context.mp_referral.Add(referral);
            _context.SaveChanges();
        }

        public IQueryable<mp_referral> Get()
        {
            return _context.mp_referral.AsQueryable();
        }
    }
}
