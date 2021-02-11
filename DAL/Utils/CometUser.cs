using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Utils
{
   public static class CometUser
    {
        private static readonly HContext _context = new HContext();

        public static string GetUser(string user_name)
        {
            //var user = _context.app_comet_user.FirstOrDefault(e => e.active == false);
            //if (user != null)
            //{
            //    //make user active
            //}

            if (user_name == "ico.cghpi@gmail.com")
            {
                return "superhero5";   
            }
            else
            {
                return "superhero4";
            }
        }
    }
}
