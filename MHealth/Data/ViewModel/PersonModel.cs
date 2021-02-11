using DAL.Models;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class PersonModel
    {
        public PersonModel()
        {

        }


        public PersonModel(mp_profile profile)
        {
            id = profile.id;
            first_name = profile.first_name;
            last_name = profile.last_name;
            fullName = string.Format("{0} {1}", profile.first_name, profile.last_name);
            preferred_name = profile.preferred_name;
            title = profile.unique_id.ToString("D10");
            imageUrl = FileUtil.GetImageLocation(profile.user_id);
            about = profile.about;
            role = "client";
            email = profile.email;
            dob = profile.dob;
            address = profile.address;
            phone = profile.phone;
            country = profile.country;
            state = profile.state;
            city = profile.city;
            marital_status = profile.marital_status;
            education_level = profile.education_level;
        }

        public PersonModel(mp_clinician clinician)
        {
            id = clinician.id;
            first_name = clinician.first_name;
            last_name = clinician.last_name;
            fullName = string.Format("{0} {1}", clinician.first_name, clinician.last_name);
            preferred_name = clinician.preferred_name;
            title = Options.GetLookupName(clinician.area_of_interest.Value);
            imageUrl = FileUtil.GetImageLocation(clinician.user_id);
            about = clinician.about;
            role = "clinician";
            email = clinician.email;
            dob = clinician.dob;
            address = clinician.address;
            phone = clinician.phone;
            country = clinician.country;
            state = clinician.state;
            city = clinician.city;
            marital_status = clinician.marital_status;
            education_level = clinician.education_level;
        }
        public Guid id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string fullName { set; get; }
        public string preferred_name { get; set; }
        public string email { get; set; }
        public string title { set; get; }
        public string imageUrl {set;get;}
        public string about { set; get; }
        public DateTime dob { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public bool isOnline { set; get; }
        public int rating { set; get; }
        public string token { set; get; }
        public string role { set; get; }
        public int response { set; get; }
        public string user_id { set; get; }
        public int country { get; set; }
        public int state { get; set; }
        public string city { get; set; }
        public int marital_status { get; set; }
        public int? education_level { get; set; }


    }
}
