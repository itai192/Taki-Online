using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
namespace UI
{
    public static class UIHelper
    {
        public static void SavePhoto(HttpPostedFile file, string name)
        {
            string path = ConfigurationManager.AppSettings["UserPhotos"];
            if (File.Exists(path + name))
                File.Delete(path + name);
            string extention = Path.GetExtension(file.FileName);
            if (extention == ".png" || extention == ".jpg" || extention == ".jpeg")
                file.SaveAs(path + name + extention);
            else
                throw new Exception("Couldn't Upload File, file type is not supported");
        }
    }
}