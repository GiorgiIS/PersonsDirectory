using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;
using TBCPersonsDirectory.Services.Dtos.RelatedPersonDtos;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Api.Controllers
{
    [Route("api/persons/{id}/related-persons/")]
    [ApiController]
    public class PersonRelationsController : Controller
    {
        private readonly IPersonsService _personsService;
        private readonly IMapper _mapper;

        public PersonRelationsController(IPersonsService personsService, IMapper mapper)
        {
            _personsService = personsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRelatedPersons([FromRoute]int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{related-person-id}")]
        public async Task<IActionResult> GetRelatedPerson([FromRoute] int id, [FromRoute(Name = "related-person-id")] int relatedPersonId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddRelatedPersons([FromRoute] int id, RelatedPersonCreateDto relatedPersonCreateDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{related-person-id}")]
        public async Task<IActionResult> UpdatePersonRelation(
            [FromRoute] int id,
            [FromRoute(Name = "{related-person-id}")] int relatedPersonId,
            [FromBody] int relationShipId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{related-person-id}")]
        public async Task<IActionResult> RemoveRelatedPerson([FromRoute]int id, [FromRoute(Name = "{related-person-id}")] int relatedPersonId)
        {
            throw new NotImplementedException();
        }

    }
}
