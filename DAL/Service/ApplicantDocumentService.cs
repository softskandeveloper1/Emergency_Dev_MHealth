using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ApplicantDocumentService: IApplicantDocumentService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_applicant_document applicant_doc)
        {
            _context.mp_applicant_document.Add(applicant_doc);
            _context.SaveChanges();
        }


        public int AddApplicantDocuments(List<mp_applicant_document> documents)
        {
            _context.mp_applicant_document.AddRange(documents);
            return _context.SaveChanges();
        }

        public mp_applicant_document Get(int id)
        {
            return _context.mp_applicant_document.FirstOrDefault(e => e.id == id);
        }


        public IQueryable<mp_applicant_document> Get()
        {
            return _context.mp_applicant_document.AsQueryable();
        }
    }
}
