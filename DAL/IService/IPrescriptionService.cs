using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IPrescriptionService
    {
        long AddPrescription(mp_prescription prescription);
        IQueryable<mp_prescription> Get();
        void AddPrescriptionDrugs(List<mp_prescription_drug> prescription_Drugs);
    }
}
