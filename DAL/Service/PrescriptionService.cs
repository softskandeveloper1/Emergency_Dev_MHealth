using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class PrescriptionService: IPrescriptionService
    {
        private readonly HContext _context = new HContext();

        public long AddPrescription(mp_prescription prescription)
        {
            prescription.created_at = DateTime.Now;
            _context.mp_prescription.Add(prescription);
            _context.SaveChanges();

            return prescription.id;
        }

        public IQueryable<mp_prescription> Get()
        {
            return _context.mp_prescription.AsQueryable();
        }

        public void AddPrescriptionDrugs(List<mp_prescription_drug> prescription_Drugs)
        {
            foreach(var prescription_drug in prescription_Drugs)
            {
                _context.mp_prescription_drug.Add(prescription_drug);
            }
            _context.SaveChanges();
        }
    }
}
