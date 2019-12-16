using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TBCPersonsDirectory.Services.Interfaces
{
    public interface IPictureService
    {
        Task Upload(IFormFile file, string path, string fileName);
    }
}
