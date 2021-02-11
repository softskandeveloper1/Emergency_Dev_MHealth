using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface ICreditService
    {
        void Add(mp_credit credit);
        IQueryable<mp_credit> Get();
    }
}
