using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IProfileMatchService
    {
        Guid Add(mp_profile_match profile_match);
        IQueryable<mp_profile_match> Get();
    }
}
