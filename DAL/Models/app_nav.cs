using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class app_nav
    {
        public int id { get; set; }
        public string name { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string icon { get; set; }
        public int order { get; set; }
        public int active { get; set; }
        public string roles { get; set; }
    }
}
