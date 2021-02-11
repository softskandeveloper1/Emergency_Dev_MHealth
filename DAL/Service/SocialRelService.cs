using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Service
{
    public class SocialRelService : ISocialRelService
    {

        private readonly HContext _context = new HContext();
        public async Task<List<mp_social_relationship>> GetSocialRelationships()
        {
            return await _context.mp_social_relationship.Include(m => m.profile_).ToListAsync();
        }

        public async Task<mp_social_relationship> GetSocialRelationship(int id)
        {
            return await _context.mp_social_relationship
                .Include(m => m.profile_)
                .FirstOrDefaultAsync(m => m.id == id);
        }

        public async Task<int> AddSocialRelationship(mp_social_relationship relationship)
        {
            _context.Add(relationship);
            return await _context.SaveChangesAsync();
        }

        public int UpdateSocialRelationship(mp_social_relationship relationship)
        {
            var existing = _context.mp_social_relationship.FirstOrDefault(m => m.id == relationship.id);
            if (existing != null)
            {
                //do update
                relationship.created_at = existing.created_at;
                relationship.created_by = existing.created_by;
                _context.Entry(existing).CurrentValues.SetValues(relationship);
                return _context.SaveChanges();
            }
            return 0;
        }

        public async Task<int> RemoveSocialRelationship(int id)
        {
            //TODO add delete flags instead of removing the object entirely
            var mp_social_relationship = await _context.mp_social_relationship.FindAsync(id);
            _context.mp_social_relationship.Remove(mp_social_relationship);
            return await _context.SaveChangesAsync();
        }

        public bool RelationshipExists(int id)
        {
            return _context.mp_social_relationship.Any(e => e.id == id);
        }
    }
}