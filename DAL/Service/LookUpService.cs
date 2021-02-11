using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
    public class LookUpService : ILookUpService
    {
        private readonly HContext _context = new HContext();

        public async Task<List<mp_lookup>> GetLookups()
        {
            return await _context.mp_lookup.Where(m => m.deleted == 0).ToListAsync();
        }

        public async Task<mp_lookup> GetLookUpById(int id)
        {
            return await _context.mp_lookup.FirstOrDefaultAsync(m => m.id == id);
        }

        public async Task<List<mp_lookup>> GetLookUpByCategory(string category)
        {
            return await _context.mp_lookup.Where(m => m.category == category).ToListAsync();
        }

        public async Task<mp_lookup> GetLookUpByValueAndCategory(string value, string category)
        {
            var lookup = _context.mp_lookup.ToList();
            return await _context.mp_lookup.FirstOrDefaultAsync(m => m.value == value && m.category == category);
        }

        public async Task<int> AddLookUp(mp_lookup lookup)
        {
            _context.Add(lookup);
            return await _context.SaveChangesAsync();
        }

        public int UpdateLookUp(mp_lookup lookup)
        {
            var existing = _context.mp_lookup.FirstOrDefault(m => m.id == lookup.id);
            if (existing != null)
            {
                //do update
                lookup.deleted = 0;
                _context.Entry(existing).CurrentValues.SetValues(lookup);
                return _context.SaveChanges();
            }
            return 0;
        }

        public async Task<int> RemoveLookUp(long id)
        {
            var mp_lookup = await _context.mp_lookup.FindAsync(id);
            mp_lookup.deleted = 0;
            _context.Entry(mp_lookup).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public bool LookUpExists(long id)
        {
            return _context.mp_lookup.Any(e => e.id == id);
        }
    }
}