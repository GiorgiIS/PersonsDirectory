using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Application
{
    public class PictureService : IPictureService
    {
        private readonly IPictureUploader _pictureUploader;

        private readonly IHostingEnvironment _env;

        public PictureService(IPictureUploader pictureUploader, IHostingEnvironment env)
        {
            _pictureUploader = pictureUploader;
            _env = env;
        }

        public async Task Upload(IFormFile file, string subPath, string fileName)
        {
            if (file.Length > 2 * 1024 * 1024)
            {
                throw new ArgumentException("Size limit is exceed");
            }

            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                bytes = ms.ToArray();
            }
            string extension;

            try
            {
                extension = "." + file.FileName.Split('.')[^1];
                fileName += extension;
            }
            catch (Exception ex)
            {
                // todo: handle exception
                throw;
            }

            string filePath = Path.Combine(_env.ContentRootPath, $"Files/Images/{subPath}");

            System.IO.Directory.CreateDirectory(filePath);

            var result = await _pictureUploader.UploadAsync(bytes, fileName, $"Files/Images/{subPath}");
        }
    }
}
