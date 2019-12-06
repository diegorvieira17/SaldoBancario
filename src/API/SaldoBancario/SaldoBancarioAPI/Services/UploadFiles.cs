using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace SaldoBancarioAPI.Services
{
    public static class UploadFiles
    {
        public static string Upload(IFormFile file)
        {
            MemoryStream ms = new MemoryStream();
            file.OpenReadStream().CopyTo(ms)
;
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var fileName = $"{Guid.NewGuid()} .ofx";

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var filePath = Path.Combine(folder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return filePath;
        }
    }
}
