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
    public class Helper
    {
        UserManager<User> UserManager { get; set; }

        public Helper(UserManager<User> userManager)
        {
            UserManager = userManager;
        }
        public async Task<string> CreateUserMediaDirectoryAsync(User User, string MediaDirectoryName)
        {
            string Role = (await UserManager.GetRolesAsync(User)).First();
            string folderPath =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    Role,
                    User.Id,
                    MediaDirectoryName
                    );
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            return folderPath;
        }
        public async Task UploadMediaAsync
            (User User, string MediaDirectoryName, string FileName, IFormFile Media)
        {
            FileStream fileStream = new(
                    Path.Combine(
                        await CreateUserMediaDirectoryAsync(User, MediaDirectoryName),
                        FileName),
                    FileMode.Create)
            {
                Position = 0
            };
            Media.CopyTo(fileStream);
            fileStream.Close();
        }

        public async Task DeleteMediaAsync
            (User User, string MediaDirectoryName, string FileName)
        {
            string Role = (await UserManager.GetRolesAsync(User)).First();
            string FilePath =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    Role,
                    User.Id,
                    MediaDirectoryName,
                    FileName);
            File.Delete(FilePath);
        }
    }
}

