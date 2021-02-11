using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IEmploymentService
    {
        void Add(mp_employment employment);
        IQueryable<mp_employment> Get();
        mp_employment Get(int id);
        void Update(mp_employment employment);
    }
}

