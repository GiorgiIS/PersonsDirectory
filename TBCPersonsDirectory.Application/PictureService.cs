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

        public PictureService(IPictureUploader pictureUploader)
        {
            _pictureUploader = pictureUploader;
        }


        public async Task Upload(IFormFile file)
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
            string fileName;

            try
            {
                extension = "." + file.FileName.Split('.')[^1];
                fileName = Guid.NewGuid().ToString() + extension;
            }
            catch (Exception ex)
            {
                // todo: handle exception
                throw;
            }

            var result = await _pictureUploader.UploadAsync(bytes, fileName, "Files/Images");
        }
    }
}
