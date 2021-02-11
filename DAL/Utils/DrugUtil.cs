using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Utils
{
    public static class DrugUtil
    {
        private static readonly HContext _context = new HContext();

        public static IQueryable<mp_lk_drug> GetDrugs()
        {
            return _context.mp_lk_drug.AsQueryable();
        }

        public static string GetDrugName(int id)
        {
            var drug = _context.mp_lk_drug.FirstOrDefault(e => e.id == id);
            return drug != null ? drug.name : "";
        }
    }
}
