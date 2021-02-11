using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
   public interface IProfileHMOService
    {
        void AddOrUpdate(mp_profile_hmo hmo);
        mp_profile_hmo GetProfileHMO(Guid profile_id);
        IQueryable<mp_profile_hmo> Get();
    }
}
