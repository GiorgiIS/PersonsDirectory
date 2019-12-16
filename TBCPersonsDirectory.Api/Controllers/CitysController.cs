using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Api.Controllers
{
    [Route("api/citys/")]
    [ApiController]
    public class CitysController : Controller
    {
        private readonly ICitysService _citysService;

        public CitysController(ICitysService citysService)
        {
            _citysService = citysService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_citysService.GetAll());
        }
    }
}
