using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class EmergencyContactService: IEmergencyContactService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_emergency_contact emergency_Contact)
        {
            emergency_Contact.created_at = DateTime.Now;
            _context.mp_emergency_contact.Add(emergency_Contact);
            _context.SaveChanges();
        }

        public IQueryable<mp_emergency_contact> Get()
        {
            return _context.mp_emergency_contact.AsQueryable();
        }

        public mp_emergency_contact Get(int id)
        {
            return _context.mp_emergency_contact.FirstOrDefault(e => e.id == id);
        }

        public void Update(mp_emergency_contact emergency_Contact)
        {
            var old = _context.mp_emergency_contact.FirstOrDefault(e => e.id == emergency_Contact.id);
            emergency_Contact.created_at = old.created_at;

            _context.Entry(old).CurrentValues.SetValues(emergency_Contact);
            _context.SaveChanges();
        }
    }
}
