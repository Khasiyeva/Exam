﻿using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AnyarMVC.Helpers
{
    public static class FileManager
    {
        public static string Upload(this IFormFile file, string envPath, string folderName)
        {
            string filname = file.FileName;
            if (filname.Length > 64)
            {
                filname = filname.Substring(filname.Length - 64);
            }
            filname = Guid.NewGuid().ToString() + filname;


            string path = envPath + folderName + filname;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filname;
        }

        public static bool CheckContent(this IFormFile file,string content)
        {
            return file.ContentType.Contains(content);
        }

        public static bool CheckLength(this IFormFile file, int length)
        {
            return file.Length<=length;
        }

        public static bool DeleteFile(string imgUrl, string envPath, string folderName)
        {
            string path = envPath + folderName + imgUrl;

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }

    }
}
