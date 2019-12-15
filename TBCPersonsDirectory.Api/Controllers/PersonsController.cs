using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBCPersonsDirectory.Services;
using TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Api.Controllers
{
    [Route("api/persons/")]
    [ApiController]
    public class PersonsController : Controller
    {
        private readonly IPersonsService _personsService;

        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
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

            if (person == null)
            {
                return NotFound(id);
            }

            return Ok(person);
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

        [HttpPut("{id}/phones/{phone-number-id}")]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute] int id, [FromRoute(Name = "phone-number-id")] int phoneNumberId, PhoneNumberUpdateDto phoneNumberUpdateDto)
        {
            if (!_personsService.PersonHasPhone(id, phoneNumberId))
            {
                return NotFound();
            }

            _personsService.UpdatePersonPhone(id, phoneNumberId, phoneNumberUpdateDto);

            return Ok();
        }

        [HttpDelete("{id}/phones/{phone-number-id}")]
        public async Task<IActionResult> RemovePhoneNumber([FromRoute]int id, [FromRoute(Name = "phone-number-id")] int phoneNumberId)
        {
            if (!_personsService.PersonHasPhone(id, phoneNumberId))
            {
                return NotFound();
            }

            _personsService.RemovePersonsPhone(id, phoneNumberId);

            return NoContent();
        }

        [HttpPost("{id}/phones")]
        public async Task<IActionResult> AddPhonesForPerson([FromRoute]int id, PhoneNumberCreateDto phoneNumberCreateDto)
        {
            if (!_personsService.Exists(id))
            {
                return NotFound(id);
            }

            _personsService.AddPhoneNumber(id, phoneNumberCreateDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = _personsService.GetById(id);

            if (person == null)
            {
                return NotFound(id);
            }

            _personsService.Delete(id);

            return NoContent();
        }

        [HttpPost("{id}/connected-persons/")]
        public async Task<IActionResult> AddConnectedPerson([FromRoute] int id, [FromBody]PersonConnectionsCreateDto relatedPersonCreateDto)
        {
            if (!_personsService.Exists(id) || !_personsService.Exists(relatedPersonCreateDto.TargetPersonId))
                return NotFound();

            if (!_personsService.PersonConnectionTypeIsValid(relatedPersonCreateDto.ConnectionTypeId))
                return UnprocessableEntity();

            if (_personsService.PersonHasConnection(id, relatedPersonCreateDto.TargetPersonId))
                return Conflict();

            _personsService.CreateConnection(id, relatedPersonCreateDto);

            return Ok();
        }

        [HttpPut("{id}/connected-persons/{connected-person-id}")]
        public async Task<IActionResult> UpdateConnectionType(
            [FromRoute] int id,
            [FromRoute(Name = "connected-person-id")] int targetPersonId,
            [FromBody] int relationShipId)
        {

            if (!_personsService.Exists(id) || !_personsService.Exists(targetPersonId)
               || !_personsService.PersonHasConnection(id, targetPersonId))
            {
                return NotFound();
            }

            if (!_personsService.PersonConnectionTypeIsValid(relationShipId))
            {
                return UnprocessableEntity();
            }

            _personsService.UpdatePersonConnection(id, targetPersonId, relationShipId);

            return Ok();
        }

        [HttpDelete("{id}/connected-persons/{connected-person-id}")]
        public async Task<IActionResult> RemoveConnection([FromRoute]int id, [FromRoute(Name = "connected-person-id")] int targetPersonId)
        {
            if (!_personsService.Exists(id) || !_personsService.Exists(targetPersonId)
              || !_personsService.PersonHasConnection(id, targetPersonId))
            {
                return NotFound();
            }

            _personsService.RemovePersonConnection(id, targetPersonId);

            return NoContent();
        }
    }
}
