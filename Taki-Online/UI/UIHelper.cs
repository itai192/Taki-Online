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
        static string PhotoPath= ConfigurationManager.AppSettings["UserPhotos"];
        public static void SavePhoto(HttpPostedFile file, string name)
        {
            string extention = Path.GetExtension(file.FileName);
            if (File.Exists(PhotoPath + name +extention))
                File.Delete(PhotoPath + name+extention);
            if (extention == ".png" || extention == ".jpg" || extention == ".jpeg")
                file.SaveAs(PhotoPath + name + extention);
            else
                throw new Exception("Couldn't Upload File, file type is not supported");
        }
        public static void DeletePhoto(string name)
        {
            if (File.Exists(PhotoPath + name))
                File.Delete(PhotoPath + name);
        }
        public static string RenamePhoto(string FileName, string renameTo)
        {
            string extention = Path.GetExtension(FileName);
            File.Move(PhotoPath + FileName, PhotoPath + renameTo + extention);
            return renameTo + extention;
        }
    }
}