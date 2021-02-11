using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface ICoupleScreeningService
    {
        void Add(mp_couple_screening screening);
        IQueryable<mp_couple_screening> Get();
    }
}
