using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IFamilyIntakeService
    {
        void Add(mp_family_intake intake);
        IQueryable<mp_family_intake> GetClientIntkakes(Guid client_id);
        IQueryable<mp_family_intake> Get();
        mp_family_intake Get(int id);
        void Update(mp_family_intake intake);
    }
}
