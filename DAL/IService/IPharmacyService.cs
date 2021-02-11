using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IPharmacyService
    {
        void Add(mp_pharmacy pharmacy);
        IQueryable<mp_pharmacy> Get();
        mp_pharmacy GetById(int id);
    }
}
