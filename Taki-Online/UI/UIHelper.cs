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
        public static string SavePhoto(HttpPostedFile file, string name)
        {
            string extention = Path.GetExtension(file.FileName);
            DeletePhoto(name + extention);
            if (extention == ".png" || extention == ".jpg" || extention == ".jpeg")
            {
                file.SaveAs(Path.GetFullPath(PhotoPath) + name + extention);
                return name + extention;
            }
            else
                throw new Exception("Couldn't Upload File, file type is not supported");
        }
        public static void DeletePhoto(string name)
        {
            if (File.Exists(Path.GetFullPath(PhotoPath) + name))
                File.Delete(Path.GetFullPath(PhotoPath) + name);
        }
        public static string RenamePhoto(string FileName, string renameTo)
        {
            string extention = Path.GetExtension(FileName);
            File.Move(Path.GetFullPath(PhotoPath) + FileName, Path.GetFullPath(PhotoPath) + renameTo + extention);
            return renameTo + extention;
        }
    }
}