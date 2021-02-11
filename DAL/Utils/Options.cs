using DAL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DAL.Utils
{
    public static class Options
    {
        private static readonly HContext _context = new HContext();

        public static IQueryable<mp_lookup> Getlookups(string category)
        {
            return _context.mp_lookup.Where(e => e.category == category);
        }

        public static mp_lookup AddLookup(mp_lookup lookup)
        {
            //get the maximum lookup
            var count = _context.mp_lookup.Count();
            lookup.id = count + 1;
            lookup.deleted = 0;

            _context.mp_lookup.Add(lookup);
            _context.SaveChanges();

            return lookup;
        }

        public static IQueryable<mp_lk_appointment_type> GetAppointmentTypes()
        {
            return _context.mp_lk_appointment_type.Where(e=>e.active==1).AsQueryable();
        }

        public static IQueryable<mp_lk_appointment_service> GetAppointmentServices()
        {
            return _context.mp_lk_appointment_service.AsQueryable();
        }

        public static IQueryable<mp_lnk_appointment_service_activity_sub> GetAppointmentSubServices()
        {
            return _context.mp_lnk_appointment_service_activity_sub.AsQueryable();
        }

        public static string GetLookupName(int id)
        {
            var lookup = _context.mp_lookup.FirstOrDefault(e => e.id == id);
            return lookup != null ? lookup.value : "";
        }

        public static int count_new_prescriptions(string user_id)
        {
            var result = 0;
            var profile = _context.mp_profile.FirstOrDefault(e => e.user_id == user_id);
            if (profile != null)
            {
                result = _context.mp_prescription.Count(e => e.profile_id == profile.id && e.viewed == 0);
            }
            return result;
        }

        public static string GetPharmacyName(int id)
        {
            var px = _context.mp_pharmacy.FirstOrDefault(e => e.id == id);
            return px != null ? px.name : "";
        }

        public static IQueryable<mp_countries> GetCountries()
        {
            return _context.mp_countries.Where(e => e.active == 1);
        }

        public static string GetCountryName(int id)
        {
            var country = _context.mp_countries.FirstOrDefault(e => e.id == id);
            return country != null ? country.name : "";
        }

        public static IQueryable<mp_states> GetStates()
        {
            return _context.mp_states.Where(e => e.country_id == 160);
        }

        public static string GetStateName(int id)
        {
            var lookup = _context.mp_states.FirstOrDefault(e => e.id == id);
            return lookup != null ? lookup.name : "";
        }

        public static IQueryable<app_nav> GetNavigations()
        {
            return _context.app_nav.Where(e => e.active == 1);
        }

        public static IQueryable<mp_lk_specialty> GetSpecialties()
        {
            return _context.mp_lk_specialty.AsQueryable();
        }

        public static int count_applicants()
        {
            return _context.mp_clinician.Count(e => e.status == 3 && e.provider_type == 177);
        }


        public static IQueryable<mp_lookup> GetQueryLookups(string query)
        {
            return _context.mp_lookup.Where(e => e.category.Contains(query));
        }

        public static IQueryable<mp_population_group> GetPopulations()
        {
            return _context.mp_population_group.AsQueryable();
        }

        public static IQueryable<mp_lk_expertise> GetExpertises()
        {
            return _context.mp_lk_expertise.AsQueryable();
        }
        public static string GetStatusBadgeColor(string status)
        {
            string color_class = "";
            switch (status.ToLower())
            {
                case "pending":
                    color_class = "badge-warning";
                    break;
                case "in-progress":
                    color_class = "badge-danger";
                    break;
                case "on-hold":
                    color_class = "badge-info";
                    break;
                case "completed":
                    color_class = "badge-success";
                    break;

            }
            return color_class;
        }

        public static List<mp_lookup> GetAllLookUps()
        {
            return _context.mp_lookup.ToList();
        }

        public static List<mp_surgery> GetSurgeyDropDown()
        {
            return null;
            //return new SelectList(new HContext().mp_surgery.ToList(), "id", "name");
        }

        public static List<mp_symptoms> GetSymptoms()
        {
            return _context.mp_symptoms.ToList();
        }

        public static IQueryable<mp_lk_appointment_activity> GetAppointmentActivities(int parent_id)
        {
            return _context.mp_lk_appointment_activity.Where(e => e.appointment_service_id == parent_id && e.active==1).OrderBy(e=>e.name);
        }

        public static IQueryable<mp_lk_appointment_activity_sub> GetAppointmentSubActivities(int parent_id)
        {
            return _context.mp_lk_appointment_activity_sub.Where(e => e.appointment_activity_id == parent_id && e.active == 1).OrderBy(e => e.name);
        }

        public static IQueryable<mp_lk_appointment_service> GetAppointmentServices(int activity_sub_id)
        {
            var service_ids = _context.mp_lnk_appointment_service_activity_sub.Where(e => e.activity_sub_id == activity_sub_id).Select(e=>e.appointment_service_id);
            return _context.mp_lk_appointment_service.Where(e => service_ids.Contains(e.id));
        }

        //flat list

        public static IQueryable<mp_lk_appointment_activity> GetAppointmentActivities()
        {
            return _context.mp_lk_appointment_activity.Where(e => e.active == 1);
        }

        public static IQueryable<mp_lk_appointment_activity_sub> GetAppointmentSubActivities()
        {
            return _context.mp_lk_appointment_activity_sub.Where(e => e.active == 1);
        }

        public static string GetAppointmentTypeName(int id)
        {
            var activity = _context.mp_lk_appointment_type.FirstOrDefault(e => e.id == id);
            return activity != null ? activity.name : "";
        }

        public static string GetAppointmentSubActivityName(int id)
        {
            var activity_sub = _context.mp_lk_appointment_activity_sub.FirstOrDefault(e => e.id == id);
            return activity_sub != null ? activity_sub.name : "";
        }

        public static string GetAppointmentActivityName(int id)
        {
            var activity = _context.mp_lk_appointment_activity.FirstOrDefault(e => e.id == id);
            return activity != null ? activity.name : "";
        }

    }
}
