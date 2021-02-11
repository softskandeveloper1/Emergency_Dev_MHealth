using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IReferralService
    {
        void Add(mp_referral referral);
        IQueryable<mp_referral> Get();
    }
}
