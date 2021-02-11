using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ConversationItem
    {
        public Guid person_id { set; get; }
        public string name { set; get; }
        public string message { set; get; }
        public string user_id { set; get; }
    }
}
