using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IProfileService
    {
        Guid Add(mp_profile profile);
        IQueryable<mp_profile> Get();
        mp_profile Get(Guid id);
        mp_profile GetProfileByUserId(string user_id);
        void Update(mp_profile profile);
        int Remove(Guid id);
        bool ProfileExists(Guid id);
        mp_profile GetByUserId(Guid id);
    }
}
