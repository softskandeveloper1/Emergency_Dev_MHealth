using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
    public class PsychosocialService : IPsychosocialService
    {
        private readonly HContext _context = new HContext();
        public async Task<List<mp_psychosocial>> GetPsychosocials()
        {
            return await _context.mp_psychosocial.ToListAsync();
        }

        public async Task<mp_psychosocial> GetPsychosocial(int id)
        {
            return await _context.mp_psychosocial
                .FirstOrDefaultAsync(m => m.id == id);
        }

        public int AddPsychosocial(mp_psychosocial psychosocial)
        {
            _context.Add(psychosocial);
            return _context.SaveChanges();
        }

        public int AddMedicalHistory(mp_medical_history history)
        {
            _context.Add(history);
            return _context.SaveChanges();
        }

        public int AddMedication(List<mp_medication> medications)
        {
            _context.AddRange(medications);
            return _context.SaveChanges();
        }

        public int AddSurgicalHistories(List<mp_surgical_history> histories)
        {
            _context.AddRange(histories);
            return _context.SaveChanges();
        }

        public int UpdatePsychosocial(mp_psychosocial psychosocial)
        {
            var existing = _context.mp_psychosocial.FirstOrDefault(m => m.id == psychosocial.id);
            if (existing != null)
            {
                //do update
                psychosocial.created_at = existing.created_at;
                psychosocial.created_by = existing.created_by;
                _context.Entry(existing).CurrentValues.SetValues(psychosocial);
                return _context.SaveChanges();
            }
            return 0;
        }

        public async Task<int> RemovePsychosocial(int id)
        {
            var mp_psychosocial = await _context.mp_psychosocial.FindAsync(id);
            _context.mp_psychosocial.Remove(mp_psychosocial);
            return await _context.SaveChangesAsync();
        }

        public bool PsychosocialExists(long id)
        {
            return _context.mp_psychosocial.Any(e => e.id == id);
        }

        public int AddFamilyHistory(mp_family_history history)
        {
            _context.Add(history);
            return _context.SaveChanges();
        }

        public int AddEducationHistory(mp_education_history history)
        {
            _context.Add(history);
            return _context.SaveChanges();
        }

        public int AddSocialRelationship(mp_social_relationship relationship)
        {
            _context.Add(relationship);
            return _context.SaveChanges();
        }

        public int AddChildren(List<mp_children> childrens)
        {
            _context.AddRange(childrens);
            return _context.SaveChanges();
        }

        public int AddEmployment(mp_employment employment)
        {
            _context.Add(employment);
            return _context.SaveChanges();
        }

        public int AddSubstanceUse(mp_substance_use substance_use)
        {
            _context.Add(substance_use);
            return _context.SaveChanges();
        }
    }
}