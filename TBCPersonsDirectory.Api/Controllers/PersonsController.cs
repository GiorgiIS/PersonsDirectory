using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Services;
using TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;
using TBCPersonsDirectory.Services.Interfaces;
using TBCPersonsDirectory.Services.Models;

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
            var serviceResponse = _personsService.GetAll(model);

            if (serviceResponse.IsSuccess)
            {
                return Ok(serviceResponse.Result);
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }

        [HttpPost]
        public IActionResult Create(PersonCreateDto personCreateDto)
        {
            var serviceResponse = _personsService.Create(personCreateDto);

            if (serviceResponse.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResponse = _personsService.GetById(id);

            if (serviceResponse.IsSuccess)
            {
                return Ok(serviceResponse.Result);
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.NOT_FOUND)
            {
                return NotFound(serviceResponse.Result);
            }

            return BadRequest(serviceResponse.Result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PersonUpdateDto personUpdateDto)
        {
            var serviceResponse = _personsService.Update(id, personUpdateDto);

            if (serviceResponse.IsSuccess)
            {
                return Ok(serviceResponse.Result);
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.NOT_FOUND)
            {
                return NotFound(serviceResponse.ServiceErrorMessage);
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }

        [HttpPut("{id}/phones/{phone-number-id}")]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute] int id, [FromRoute(Name = "phone-number-id")] int phoneNumberId, PhoneNumberUpdateDto phoneNumberUpdateDto)
        {
            var serviceResponse = _personsService.UpdatePersonPhone(id, phoneNumberId, phoneNumberUpdateDto);

            if (serviceResponse.IsSuccess)
            {
                return Ok();
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.NOT_FOUND)
            {
                return NotFound(serviceResponse.ServiceErrorMessage);
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }

        [HttpDelete("{id}/phones/{phone-number-id}")]
        public async Task<IActionResult> RemovePhoneNumber([FromRoute]int id, [FromRoute(Name = "phone-number-id")] int phoneNumberId)
        {
            var serviceResponse = _personsService.RemovePersonsPhone(id, phoneNumberId);

            if (serviceResponse.IsSuccess)
            {
                return NoContent();
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.NOT_FOUND)
            {
                return NotFound(serviceResponse.ServiceErrorMessage);
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }

        [HttpPost("{id}/phones")]
        public async Task<IActionResult> AddPhonesForPerson([FromRoute]int id, PhoneNumberCreateDto phoneNumberCreateDto)
        {
            var serviceResponse = _personsService.AddPhoneNumber(id, phoneNumberCreateDto);

            if (serviceResponse.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResponse = _personsService.Delete(id);

            if (serviceResponse.IsSuccess)
            {
                return NoContent();
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.NOT_FOUND)
            {
                return NotFound(serviceResponse.ServiceErrorMessage);
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }

        [HttpPost("{id}/connected-persons/")]
        public async Task<IActionResult> AddConnectedPerson([FromRoute] int id, [FromBody]PersonConnectionsCreateDto relatedPersonCreateDto)
        {
            var serviceResponse = _personsService.CreateConnection(id, relatedPersonCreateDto);

            if (serviceResponse.IsSuccess)
            {
                return Ok();
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.NOT_FOUND)
            {
                return NotFound(serviceResponse.ServiceErrorMessage);
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.ALREADY_EXISTS)
            {
                return Conflict(serviceResponse.ServiceErrorMessage);
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.INVALID_VALUE)
            {
                return UnprocessableEntity(serviceResponse.ServiceErrorMessage);
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }

        [HttpPut("{id}/connected-persons/{connected-person-id}")]
        public async Task<IActionResult> UpdateConnectionType(
            [FromRoute] int id,
            [FromRoute(Name = "connected-person-id")] int targetPersonId,
            [FromBody] int relationShipId)
        {
            var serviceResponse = _personsService.UpdatePersonConnection(id, targetPersonId, relationShipId);

            if (serviceResponse.IsSuccess)
            {
                return Ok();
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.NOT_FOUND)
            {
                return NotFound(serviceResponse.ServiceErrorMessage);
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.ALREADY_EXISTS)
            {
                return Conflict(serviceResponse.ServiceErrorMessage);
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.INVALID_VALUE)
            {
                return UnprocessableEntity(serviceResponse.ServiceErrorMessage);
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }

        [HttpDelete("{id}/connected-persons/{connected-person-id}")]
        public async Task<IActionResult> RemoveConnection([FromRoute]int id, [FromRoute(Name = "connected-person-id")] int targetPersonId)
        {
            var serviceResponse = _personsService.RemovePersonConnection(id, targetPersonId);

            if (serviceResponse.IsSuccess)
            {
                return NoContent();
            }

            if (serviceResponse.ServiceErrorMessage.Code == ErrorStatusCodes.NOT_FOUND)
            {
                return NotFound(serviceResponse.ServiceErrorMessage);
            }

            return BadRequest(serviceResponse.ServiceErrorMessage);
        }
    }
}
