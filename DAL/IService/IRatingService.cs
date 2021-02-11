using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IRatingService
    {
        void AddOrUpdate(mp_clinician_rating rating);
        IQueryable<mp_clinician_rating> Get();
    }
}
