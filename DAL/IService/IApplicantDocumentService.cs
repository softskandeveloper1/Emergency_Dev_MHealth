using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IApplicantDocumentService
    {
        void Add(mp_applicant_document applicant_doc);
        mp_applicant_document Get(int id);
        IQueryable<mp_applicant_document> Get();
        int AddApplicantDocuments(List<mp_applicant_document> documents);
    }
}
