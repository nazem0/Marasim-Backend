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
        public static string CreateUserMediaDirectoryAsync(string UserID, string MediaDirectoryName, string? SubDirectroy = null)
        {
            string MediaPath = "";
            if (SubDirectroy != null)
            {
                MediaPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    UserID,
                    MediaDirectoryName,
                    SubDirectroy
                    );
            }
            else
            {
                    MediaPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        UserID,
                        MediaDirectoryName
                        );
            }
            string folderPath = MediaPath;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }
        public static void UploadMediaAsync
            (string UserID, string MediaDirectoryName, string FileName, IFormFile Media, string? SubDirectroy = null)
        {
            FileStream fileStream = new(
                    Path.Combine(
                       CreateUserMediaDirectoryAsync(UserID, MediaDirectoryName, SubDirectroy),
                        FileName),
                    FileMode.Create)
            {
                Position = 0
            };
            Media.CopyTo(fileStream);
            fileStream.Close();
        }

        public static void DeleteMediaAsync
            (string UserID, string MediaDirectoryName, string FileName, string? SubDirectory = null)
        {
            string FilePath = "";
            if (SubDirectory != null)
            {
                FilePath =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    UserID,
                    MediaDirectoryName,
                    SubDirectory,
                    FileName);

            }
            else
            {
                FilePath =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    UserID,
                    MediaDirectoryName,
                    FileName);
            }
            
            File.Delete(FilePath);
        }
    }
}

