using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ChildrenService: IChildrenService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_children children)
        {
            children.created_at = DateTime.Now;
            _context.mp_children.Add(children);
            _context.SaveChanges();
        }

        public IQueryable<mp_children> Get()
        {
            return _context.mp_children.AsQueryable();
        }

        public mp_children Get(int id)
        {
            return _context.mp_children.FirstOrDefault(e => e.id == id);
        }

        public void Update(mp_children children)
        {
            var old = _context.mp_children.FirstOrDefault(e => e.id == children.id);
            children.created_at = old.created_at;

            _context.Entry(old).CurrentValues.SetValues(children);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var children = _context.mp_children.FirstOrDefault(e => e.id == id);
            if (children != null)
            {
                _context.mp_children.Remove(children);
                _context.SaveChanges();
            }         
        }
    }
}
