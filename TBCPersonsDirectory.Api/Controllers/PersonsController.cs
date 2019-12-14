using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Api.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : Controller
    {
        private readonly IPersonsService _personsService;
        private readonly IMapper _mapper;

        public PersonsController(IPersonsService personsService, IMapper mapper)
        {
            _personsService = personsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_personsService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateDto personCreateDto)
        {
            _personsService.Create(personCreateDto);
            return Ok();
        }

    }
}
