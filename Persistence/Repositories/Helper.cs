﻿using Microsoft.AspNetCore.Http;

namespace Persistence.Repositories
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
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}

