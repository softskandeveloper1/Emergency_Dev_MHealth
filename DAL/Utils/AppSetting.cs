using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Utils
{
    public static class AppSetting
    {
        private static readonly HContext _context = new HContext();

        public static string GetValue(string key)
        {
            var setting = _context.app_setting.FirstOrDefault(e => e.key == key);

            return setting != null ? setting.value : "";
        }
    }
}
