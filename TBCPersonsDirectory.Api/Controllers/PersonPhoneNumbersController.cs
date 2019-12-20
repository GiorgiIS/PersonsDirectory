using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Api.Controllers
{
    [Route("api/persons/{id}/phones/")]
    [ApiController]
    public class PersonPhoneNumbersController : Controller
    {
        private readonly IPersonsService _personsService;
        private readonly IPersonPhoneNumberService _personPhoneNumberService;
        private readonly IMapper _mapper;

        public PersonPhoneNumbersController(
            IPersonsService personsService,
            IPersonPhoneNumberService personPhoneNumberService,
            IMapper mapper)
        {
            _personsService = personsService;
            _personPhoneNumberService = personPhoneNumberService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhonesForPerson([FromRoute]int id)
        {
            if (!_personsService.Exists(id))
            {
                return NotFound(id);
            }

            var phones = _personPhoneNumberService.GetAllByPersonId(id);

            return Ok(phones);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhonesForPerson([FromRoute]int id, List<PhoneNumberCreateDto> phoneNumberCreateDtos)
        {
            if (!_personsService.Exists(id))
            {
                return NotFound(id);
            }

            _personPhoneNumberService.Create(id, phoneNumberCreateDtos);

            return Ok();
        }

        [HttpPut("{phone-number-id}")]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute] int id, [FromRoute(Name = "phone-number-id")] int phoneNumberId, PhoneNumberUpdateDto phoneNumberUpdateDto)
        {
            if (!_personPhoneNumberService.Exists(id, phoneNumberId))
            {
                return NotFound();
            }

            _personPhoneNumberService.Update(id, phoneNumberId, phoneNumberUpdateDto);

            return Ok();
        }

        [HttpDelete("{phone-number-id}")]
        public async Task<IActionResult> RemovePhoneNumber([FromRoute]int id, [FromRoute(Name = "phone-number-id")] int phoneNumberId)
        {
            if (!_personPhoneNumberService.Exists(id, phoneNumberId))
            {
                return NotFound();
            }

            _personPhoneNumberService.Delete(id, phoneNumberId);

            return NoContent();
        }
    }
}
