using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class PasswordModel
    {
        public string OldPassword { set; get; }
        public string NewPassword { set; get; }
        public string UserName { set; get; }
    }
}
