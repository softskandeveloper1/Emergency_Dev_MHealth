using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
    public class SubstanceUseService : ISubstanceUseService
    {
        private readonly HContext _context = new HContext();

        //get list of mp_substance_use
        public async Task<List<mp_substance_use>> GetSubstanceUses()
        {
            return await _context.mp_substance_use.ToListAsync();
        }

        //gets single mp_substance_use
        public async Task<mp_substance_use> GetSubstanceUse(int id)
        {
            return await _context.mp_substance_use
               .FirstOrDefaultAsync(m => m.id == id);
        }

        //adds mp_substance_use to the db
        public async Task<int> AddSubstanceUse(mp_substance_use substance_use)
        {
            _context.Add(substance_use);
            return await _context.SaveChangesAsync();
        }

        //adds a range of mp_substance_use to the db
        public async Task<int> AddSubstanceUses(List<mp_substance_use> substance_uses)
        {
            _context.AddRange(substance_uses);
            return await _context.SaveChangesAsync();
        }

        //updates mp_substance_use record
        //returns 0 if record was not found
        public int UpdateSubstanceUse(mp_substance_use substance_use)
        {
            var existing = _context.mp_substance_use.FirstOrDefault(m => m.id == substance_use.id);
            if (existing != null)
            {
                //do update
                substance_use.created_at = existing.created_at;
                substance_use.created_by = existing.created_by;
                _context.Entry(existing).CurrentValues.SetValues(substance_use);
                return _context.SaveChanges();
            }
            return 0;
        }

        public async Task<int> RemoveSubstanceUse(int id)
        {
            var mp_substance_use = await _context.mp_substance_use.FindAsync(id);
            _context.mp_substance_use.Remove(mp_substance_use);
            return await _context.SaveChangesAsync();
        }

        public bool substanceUseExists(long id)
        {
            return _context.mp_substance_use.Any(e => e.id == id);
        }
    }
}