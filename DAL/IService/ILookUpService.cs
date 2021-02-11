using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.IService
{
    public interface ILookUpService
    {
        Task<List<mp_lookup>> GetLookups();
        Task<mp_lookup> GetLookUpById(int id);
        Task<List<mp_lookup>> GetLookUpByCategory(string category);
        Task<int> AddLookUp(mp_lookup lookup);
        int UpdateLookUp(mp_lookup lookup);
        Task<int> RemoveLookUp(long id);
        bool LookUpExists(long id);
        Task<mp_lookup> GetLookUpByValueAndCategory(string value, string category);
    }
}