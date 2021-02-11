using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Entities
{
    public class PayStackSettings
    {
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public string APIBaseUrl { get; set; }
    }
}
