using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBCPersonsDirectory.Common;

namespace TBCPersonsDirectory.Services.Interfaces
{
    public interface IPictureUploader
    {
        Task<UploadResult> UploadAsync(byte[] image, string filename, string location);
    }
}
