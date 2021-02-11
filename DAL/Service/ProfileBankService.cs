using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ProfileBankService: IProfileBankService
    {
        private readonly HContext _context = new HContext();

        public void AddOrUpdate(mp_profile_bank bank)
        {
            var old = _context.mp_profile_bank.FirstOrDefault(e => e.id == bank.id);
            if (old != null)
            {
                bank.created_at = old.created_at;
                bank.created_by = old.created_by;
                bank.updated_at = DateTime.Now;

                _context.Entry(old).CurrentValues.SetValues(bank);
            }
            else
            {
                bank.created_at = DateTime.Now;
                _context.mp_profile_bank.Add(bank);
            }
            _context.SaveChanges();
        }

        public mp_profile_bank GetProfileBank(Guid profile_id)
        {
            return _context.mp_profile_bank.FirstOrDefault(e => e.profile_id == profile_id);
        }

        public IQueryable<mp_profile_bank> Get()
        {
            return _context.mp_profile_bank.AsQueryable();
        }
    }
}
