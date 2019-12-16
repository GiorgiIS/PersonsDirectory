using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Application
{
    public class PictureUploader : IPictureUploader
    {
        public async Task<UploadResult> UploadAsync(byte[] image, string filename, string location)
        {
            if (ImageChecker.IsValidImage(image))
            {
                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), location, filename);
                    await File.WriteAllBytesAsync(path, image);
                }
                catch (Exception e)
                {
                    return new UploadResult
                    {
                        Success = false,
                        Error = new UploadResult.UploadError
                        {
                            Code = "INVALID_FORMAT",
                            Description = e.Message
                        }
                    };
                }
                return new UploadResult
                {
                    Success = true,
                    LocalUrl = $"{location}/{filename}",
                    FileName = filename
                };
            }
            return new UploadResult
            {
                Success = false,
                Error = new UploadResult.UploadError
                {
                    Code = "INVALID_FORMAT",
                    Description = "Valid image formats are bmp, jpg, png, jpeg"
                }
            };
        }
    }
}
