using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Common.Api;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Api.Controllers
{
    [Route("api/persons/{id}/pictures/")]
    [ApiController]
    public class PictureController : BaseAPiController
    {
        private readonly IPersonsService _personsService;
        private readonly IWebHostEnvironment _env;

        public PictureController(IPersonsService personsService, IWebHostEnvironment env)
        {
            _personsService = personsService;
            _env = env;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> Post(int id, IFormFile file)
        {
            var serviceResponse = await _personsService.UploadPicture(id, file);

            if (serviceResponse.IsSuccess)
            {
                var updateImagePathResponse = await _personsService.UpdatePersonImagePath(id, serviceResponse.Result.ToString());

                if (updateImagePathResponse.IsSuccess)
                {
                    return Ok();
                }

                return TransformServiceErrorToHttpStatusCode(updateImagePathResponse.ServiceErrorMessage);
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var filePath = Directory.GetFiles(Path.Combine(_env.ContentRootPath, $"Files/Images/{id}/"), $"Person_{id}_Image*");
                return Ok(filePath);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}