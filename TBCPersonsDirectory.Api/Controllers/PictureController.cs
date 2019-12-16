using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Api.Controllers
{
    [Route("api/images/")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly IPictureService _pictureService;

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [Produces("application/json")]
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            await _pictureService.Upload(file);

            return Ok();
        }
    }
}
