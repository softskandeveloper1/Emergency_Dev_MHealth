using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAL.Utils
{
    public static class FileUtil
    {
        public static string GetImageLocation(string user_id)
        {
            var file_path = Path.Combine("wwwroot", "images", "profile", user_id + ".jpg");
            if (File.Exists(file_path))
            {
                return "/images/profile/"+ user_id + ".jpg";
            }
            else
            {
                return "/images/profile/anonymous.jpg";
            }
        }
    }
}
