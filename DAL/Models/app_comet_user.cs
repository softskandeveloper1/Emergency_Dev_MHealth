using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class app_comet_user
    {
        public int id { get; set; }
        public string username { get; set; }
        public bool active { get; set; }
    }
}
