using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
   public interface IProfileBankService
    {
        void AddOrUpdate(mp_profile_bank bank);
        mp_profile_bank GetProfileBank(Guid profile_id);
        IQueryable<mp_profile_bank> Get();
    }
}
