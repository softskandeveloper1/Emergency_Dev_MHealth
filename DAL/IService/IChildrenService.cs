using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IChildrenService
    {
        void Add(mp_children children);
        IQueryable<mp_children> Get();
        mp_children Get(int id);
        void Update(mp_children children);
        void Delete(int id);
    }
}
