using DAL.Models;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class DoctorModel
    {
        public DoctorModel()
        {

        }

        public DoctorModel(mp_clinician clinician)
        {
            clinician ??= new mp_clinician();
            id = clinician.id;
            fullName = string.Format("{0} {1}", clinician.first_name, clinician.last_name);
            title = clinician.area_of_interest.HasValue ? Options.GetLookupName(clinician.area_of_interest.Value) : null;
            imageUrl = FileUtil.GetImageLocation(clinician.user_id);
            about = clinician.about;
            address = clinician.address;
        }

        public Guid id { get; set; }
        public string fullName { set; get; }
        public string title { set; get; }
        public string imageUrl { set; get; }
        public string about { set; get; }
        public bool isOnline { set; get; }
        public int rating { set; get; }
        public string token { set; get; }
        public string address { set; get; }
    }
}
