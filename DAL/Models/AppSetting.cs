using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class AppSetting
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
