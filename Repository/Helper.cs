using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class Helper
    {
        public static string CreateUserMediaDirectoryAsync(string UserID, string MediaDirectoryName)
        {
            string folderPath =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    UserID,
                    MediaDirectoryName
                    );
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }
        public static void UploadMediaAsync
            (string UserID, string MediaDirectoryName, string FileName, IFormFile Media)
        {
            FileStream fileStream = new(
                    Path.Combine(
                       CreateUserMediaDirectoryAsync(UserID, MediaDirectoryName),
                        FileName),
                    FileMode.Create)
            {
                Position = 0
            };
            Media.CopyTo(fileStream);
            fileStream.Close();
        }

        public static void DeleteMediaAsync
            (string UserID, string MediaDirectoryName, string FileName)
        {
            string FilePath =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    UserID,
                    MediaDirectoryName,
                    FileName);
            File.Delete(FilePath);
        }
    }
}

