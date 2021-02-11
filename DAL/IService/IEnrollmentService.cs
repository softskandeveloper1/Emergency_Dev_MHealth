using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IEnrollmentService
    {
        void Add(mp_enrollment enrollment);
        mp_enrollment Get(int id);
        mp_enrollment Get(Guid profile_id);
        IQueryable<mp_enrollment> Get();
        void Update(mp_enrollment enrollment);
        
    }
}
