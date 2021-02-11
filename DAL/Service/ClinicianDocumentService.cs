using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Service
{
    public class ClinicianDocumentService : IClinicianDocumentService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_clinician_document applicant_doc)
        {
            _context.mp_clinician_document.Add(applicant_doc);
            _context.SaveChanges();
        }

        public int AddADocuments(List<mp_clinician_document> documents)
        {
            _context.mp_clinician_document.AddRange(documents);
            return _context.SaveChanges();
        }


        public mp_clinician_document Get(int id)
        {
            return _context.mp_clinician_document.FirstOrDefault(e => e.id == id);
        }


        public IQueryable<mp_clinician_document> Get()
        {
            return _context.mp_clinician_document.AsQueryable();
        }

        public IQueryable<mp_clinician_document> GetByClinician(Guid clinician_id)
        {
            return _context.mp_clinician_document.Where(x => x.clinician_id == clinician_id).AsQueryable();
        }

        public int UpdateProfileDocument(mp_clinician_document document)
        {
            var existing = _context.mp_clinician_document.FirstOrDefault(m => m.id == document.id);
            if (existing != null)
            {
                //do update
                document.created_at = existing.created_at;
                _context.Entry(existing).CurrentValues.SetValues(document);
                return _context.SaveChanges();
            }
            return 0;
        }

        public int RemoveProfileDocument(long id)
        {
            var mp_profile_document = _context.mp_clinician_document.Find(id);
            _context.mp_clinician_document.Remove(mp_profile_document);
            return _context.SaveChanges();
        }

        public bool profileDocumentExists(int id)
        {
            return _context.mp_clinician_document.Any(e => e.id == id);
        }
    }
}
