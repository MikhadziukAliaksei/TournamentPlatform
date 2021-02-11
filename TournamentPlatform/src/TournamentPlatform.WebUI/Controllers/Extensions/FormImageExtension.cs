using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TournamentPlatform.WebUI.Controllers.Extensions
{
    public static class FormImageExtension
    {
        public static async Task<string> SaveImage(this IFormFile logoPath, string root, string pathInRoot)
        {
            var newFileName = Guid.NewGuid() + Path.GetExtension(logoPath.FileName);
            var path = pathInRoot + newFileName;

            await using var fileStream = new FileStream(root + path, FileMode.Create);
            await logoPath.CopyToAsync(fileStream);

            return newFileName;
        }
    }
}