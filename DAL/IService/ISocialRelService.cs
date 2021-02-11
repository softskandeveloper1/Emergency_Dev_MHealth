using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.IService
{
    public interface ISocialRelService
    {
        Task<List<mp_social_relationship>> GetSocialRelationships();
        Task<mp_social_relationship> GetSocialRelationship(int id);
        Task<int> AddSocialRelationship(mp_social_relationship relationship);
        int UpdateSocialRelationship(mp_social_relationship relationship);
        Task<int> RemoveSocialRelationship(int id);
        bool RelationshipExists(int id);
    }
}