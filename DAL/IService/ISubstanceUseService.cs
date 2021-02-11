using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.IService
{
    public interface ISubstanceUseService
    {
        Task<List<mp_substance_use>> GetSubstanceUses();
        Task<mp_substance_use> GetSubstanceUse(int id);
        Task<int> AddSubstanceUse(mp_substance_use substance_use);
        Task<int> AddSubstanceUses(List<mp_substance_use> substance_uses);
        int UpdateSubstanceUse(mp_substance_use substance_use);
        Task<int> RemoveSubstanceUse(int id);
        bool substanceUseExists(long id);
    }
}