using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Service
{
    public class PsychiatricProgressNoteService : IPsychiatricProgressNoteService
    {
        private readonly HContext _context = new HContext();

        public IQueryable<mp_psychiatric_progress_note> GetNote()
        {
            return _context.mp_psychiatric_progress_note.AsQueryable();
        }

        public async Task<List<mp_psychiatric_progress_note>> Get()
        {
            return await _context.mp_psychiatric_progress_note.Include(m => m.profile_).ToListAsync();
        }

        public async Task<mp_psychiatric_progress_note> Get(long id)
        {
            return await _context.mp_psychiatric_progress_note
                .Include(m => m.profile_)
                .FirstOrDefaultAsync(m => m.id == id);
        }

        public async Task<int> Add(mp_psychiatric_progress_note progess_note)
        {
            _context.Add(progess_note);
            return await _context.SaveChangesAsync();
        }

        public int Update(mp_psychiatric_progress_note progess_note)
        {
            var existing = _context.mp_psychiatric_progress_note.FirstOrDefault(m => m.id == progess_note.id);
            if (existing != null)
            {
                //do update
                progess_note.created_at = existing.created_at;
                progess_note.created_by = existing.created_by;
                _context.Entry(existing).CurrentValues.SetValues(progess_note);
                return _context.SaveChanges();
            }
            return 0;
        }

        public async Task<int> Remove(long id)
        {
            //TODO add delete flags instead of removing the object entirely
            var mp_psychiatric_progress_note = await _context.mp_psychiatric_progress_note.FindAsync(id);
            _context.mp_psychiatric_progress_note.Remove(mp_psychiatric_progress_note);
            return await _context.SaveChangesAsync();
        }

        public bool Exists(long id)
        {
            return _context.mp_psychiatric_progress_note.Any(e => e.id == id);
        }

        public int AddPsychiatricOpd(mp_psychiatric_opd_evaluation evaluation)
        {
            _context.Add(evaluation);
            return _context.SaveChanges();
        }

        public IQueryable<mp_psychiatric_opd_evaluation> GetPsychiatricOpd()
        {
            return _context.mp_psychiatric_opd_evaluation.AsQueryable();
        }
    }
}
