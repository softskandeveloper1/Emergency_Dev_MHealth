using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Entities
{
    public class QuestionItem
    {
        public string id { set; get; }
        public string comment { set; get; }
        public string question { set; get; }
        public string model_name { set; get; }
        public string q_type { set; get; }
        public string lookup_category { set; get; }
        public List<mp_lookup> lookup_values { set; get; }
    }
}
