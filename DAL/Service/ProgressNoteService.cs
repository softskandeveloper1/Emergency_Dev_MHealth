using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
    public class ProgressNoteService : IProgressNoteService
    {
        private HContext _context = new HContext();

        public IQueryable<mp_progress_note> Get()
        {
            return  _context.mp_progress_note.AsQueryable();
        }

        public async Task<List<mp_progress_note>> GetProgressNotes()
        {
            return await _context.mp_progress_note.Include(m => m.profile_).ToListAsync();
        }

        public async Task<mp_progress_note> GetProgressNote(int id)
        {
            return await _context.mp_progress_note.FirstOrDefaultAsync(m => m.id == id);
        }

        public async Task<int> AddProgressNote(mp_progress_note note)
        {
            _context.Add(note);
            return await _context.SaveChangesAsync();
        }

        public int UpdateProgressNote(mp_progress_note note)
        {
            var existing = _context.mp_progress_note.FirstOrDefault(m => m.id == note.id);
            if (existing != null)
            {
                //do update
                note.created_at = existing.created_at;
                note.created_by = existing.created_by;
                _context.Entry(existing).CurrentValues.SetValues(note);
                return _context.SaveChanges();
            }
            return 0;
        }

        public async Task<int> RemoveProgressNote(long id)
        {
            var mp_progress_note = await _context.mp_progress_note.FindAsync(id);
            _context.mp_progress_note.Remove(mp_progress_note);
            return await _context.SaveChangesAsync();
        }

        public bool ProgressNoteExists(long id)
        {
            return _context.mp_progress_note.Any(e => e.id == id);
        }
    }
}