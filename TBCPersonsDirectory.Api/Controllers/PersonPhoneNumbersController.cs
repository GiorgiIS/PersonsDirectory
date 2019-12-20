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
        private readonly IMapper _mapper;

        public PersonPhoneNumbersController(IPersonsService personsService, IMapper mapper)
        {
            _personsService = personsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhonesForPerson([FromRoute]int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> AddPhonesForPerson([FromRoute]int id, List<PhoneNumberCreateDto> phoneNumberCreateDtos)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute]int id, PhoneNumberUpdateDto phoneNumberUpdateDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{phone-number-id}")]
        public async Task<IActionResult> RemovePhoneNumber([FromRoute]int id, [FromRoute(Name = "phone-number-id")]int phoneNumberId)
        {
            throw new NotImplementedException();
        }

    }
}
