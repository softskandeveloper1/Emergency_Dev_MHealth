using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class PopulationService: IPopulationService
    {
        private readonly HContext _context = new HContext();

        public void AddApplicantPopulationGroup(List<mp_applicant_population> populations)
        {
            foreach(var population in populations)
            {
                _context.mp_applicant_population.Add(population);
            }
            _context.SaveChanges();
        }

        public IQueryable<mp_applicant_population> GetApplicantPopulations(Guid applicant_id)
        {
            return _context.mp_applicant_population.Where(e => e.applicant_id == applicant_id).Include(e => e.population_);
        }


        public IQueryable<mp_clinician_population> GetClinicianPopulations(Guid clinician_id)
        {
            return _context.mp_clinician_population.Where(e => e.clinician_id == clinician_id).Include(e => e.population_);
        }

        public void AddClinicianPopulationGroup(List<mp_clinician_population> populations)
        {
            foreach (var population in populations)
            {
                _context.mp_clinician_population.Add(population);
            }
            _context.SaveChanges();
        }
    }
}
