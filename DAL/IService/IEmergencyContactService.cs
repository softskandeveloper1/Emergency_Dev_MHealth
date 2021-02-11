using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IEmergencyContactService
    {
        void Add(mp_emergency_contact emergency_Contact);
        IQueryable<mp_emergency_contact> Get();
        mp_emergency_contact Get(int id);
        void Update(mp_emergency_contact emergency_Contact);
    }
}
