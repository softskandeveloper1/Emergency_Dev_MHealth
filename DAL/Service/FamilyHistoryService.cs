using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class FamilyHistoryService: IFamilyHistoryService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_family_history family_History)
        {
            family_History.created_at = DateTime.Now;
            _context.mp_family_history.Add(family_History);
            _context.SaveChanges();
        }

        public IQueryable<mp_family_history> Get()
        {
            return _context.mp_family_history.AsQueryable();
        }

        public mp_family_history Get(int id)
        {
            return _context.mp_family_history.FirstOrDefault(e => e.id == id);
        }

        public void Update(mp_family_history family_History)
        {
            var old = _context.mp_family_history.FirstOrDefault(e => e.id == family_History.id);
            family_History.created_at = old.created_at;

            _context.Entry(old).CurrentValues.SetValues(family_History);
            _context.SaveChanges();
        }
    }
}
