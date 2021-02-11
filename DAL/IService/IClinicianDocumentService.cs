using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IClinicianDocumentService
    {
        void Add(mp_clinician_document applicant_doc);
        int AddADocuments(List<mp_clinician_document> documents);
        mp_clinician_document Get(int id);
        IQueryable<mp_clinician_document> Get();
        IQueryable<mp_clinician_document> GetByClinician(Guid clinician_id);
        int UpdateProfileDocument(mp_clinician_document document);
        int RemoveProfileDocument(long id);
        bool profileDocumentExists(int id);
    }
}
