using DAL.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DAL.BL
{
    public class ProviderSelection
    {
        public bool isQuestionaireCompleted(int appointment_type,Guid profile_id)
        {
            var result = true;
           
            if (appointment_type==1)
            {
                var enrollement = new EnrollmentService().Get(profile_id);
                if (enrollement == null) result = false;
            }else if (appointment_type == 2)
            {
                //var profile_service = new ProfileService();
                var values = new[] { "gender","dob" };
                var profile = new ProfileService().Get().FirstOrDefault(e=>e.id==profile_id);
               
                var profile_json = JObject.Parse(JsonConvert.SerializeObject(profile));

                foreach(var value in values)
                {
                    if (profile_json[value] == null)
                    {
                        result = false;
                    }
                }
            }
            else if (appointment_type == 3)
            {
                var values = new[] { "gender", "dob" };
                var profile = new ProfileService().Get().FirstOrDefault(e => e.id == profile_id);
                var profile_json = JObject.Parse(JsonConvert.SerializeObject(profile));

                foreach (var value in values)
                {
                    if (profile_json[value] == null)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }
    }
}
