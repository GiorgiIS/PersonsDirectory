using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Api.Controllers
{
    [Route("api/persons/")]
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
        public async Task<IActionResult> Get([FromQuery]PersonSearchModel model)
        {
            return Ok(_personsService.GetAll(model));
        }

        [HttpPost]
        public IActionResult Create(PersonCreateDto personCreateDto)
        {
            _personsService.Create(personCreateDto);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = _personsService.GetById(id);

            if (person.IsNull())
            {
                return NotFound(id);
            }

            return Ok(_personsService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonUpdateDto personUpdateDto)
        {
            if (!_personsService.Exists(id))
            {
                return NotFound(id);
            }

            _personsService.Update(id, personUpdateDto);
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = _personsService.GetById(id);

            if (person.IsNull())
            {
                return NotFound(id);
            }

            _personsService.Delete(id);

            return NoContent();
        }
    }
}
