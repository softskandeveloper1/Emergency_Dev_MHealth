using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IFamilyHistoryService
    {
        void Add(mp_family_history family_History);
        IQueryable<mp_family_history> Get();
        mp_family_history Get(int id);
        void Update(mp_family_history family_History);
    }
}
