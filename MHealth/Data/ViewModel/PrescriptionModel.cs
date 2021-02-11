using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Utils;

namespace MHealth.Data.ViewModel
{

    public class PrescriptionModel
    {
        public PrescriptionModel()
        {

        }

        public PrescriptionModel(mp_prescription prescription, mp_pharmacy mp_pharmacy)
        {
            id = prescription.id;
            profile_id = prescription.profile_id;
            created_at = prescription.created_at;
            comment = prescription.comment;
            clinician_id = prescription.clinician_id;

            if (prescription.pharmacy_id.HasValue)
            {
                pharmacy = mp_pharmacy.name;
                pharmacy_email = mp_pharmacy.email;
                pharmacy_address = mp_pharmacy.address;
                pharmacy_phone = mp_pharmacy.phone;
            }
            clinician = new DoctorModel(prescription.clinician_);
            profile = new MemberModel(prescription.profile_);
            drugs = new List<DrugModel>();

            foreach(var drug in prescription.mp_prescription_drug)
            {
                drugs.Add(new DrugModel(drug));
            }
            drug_count = drugs.Count;
        }

        public long id { get; set; }
        public Guid profile_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public string comment { get; set; }
        public Guid clinician_id { get; set; }
        public int? pharmacy_id { get; set; }
        public string pharmacy { set; get; }
        public string pharmacy_email { set; get; }
        public string pharmacy_address { set; get; }
        public string pharmacy_phone { set; get; }
        public int? viewed { get; set; }
        public int drug_count { get; set; }

        public DoctorModel clinician { get; set; }
        public MemberModel profile { get; set; }
        public List<DrugModel> drugs { get; set; }
    }
}
