using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class app_setting
    {
        public int id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }
}
