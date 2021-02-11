using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MHealth.Helper
{
    public static class ViewHelper
    {
        public static string GetApplicantFullName(this mp_applicant person)
        {
            return person.last_name.ToUpper() + ", " + person.first_name;
        }

        public static List<mp_lookup> GetAllLookUps()
        {
            return Options.GetAllLookUps();
        }

        public static SelectList GetLookUpDropDown(string category)
        {
            return new SelectList(Options.GetAllLookUps().Where(m => m.category == category), "id", "value");
        }

        public static SelectList GetSurgeyDropDown()
        {
            return null;
            //return new SelectList(new HContext().mp_surgery.ToList(), "id", "name");
        }

        public static List<mp_symptoms> GetSymptoms()
        {
            return Options.GetSymptoms();
        }

        public static DayOfWeek[] GetdayOfWeeks()
        {
            DayOfWeek[] days = {
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday };
            return days;
        }
    }
}