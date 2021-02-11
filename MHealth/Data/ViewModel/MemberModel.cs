using DAL.Models;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class MemberModel
    {
        public MemberModel()
        {

        }

        public MemberModel(mp_profile profile)
        {
            id = profile.id;
            fullName = string.Format("{0} {1}", profile.first_name, profile.last_name);
            title = profile.unique_id.ToString("D10");
            imageUrl = FileUtil.GetImageLocation(profile.user_id);
            about = profile.about;
        }




        public Guid id { get; set; }
        public string fullName { set; get; }
        public string title { set; get; }
        public string imageUrl {set;get;}
        public string about { set; get; }
        public bool isOnline { set; get; }
        public int rating { set; get; }
        public string token { set; get; }
    }
}
