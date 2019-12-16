using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Common.Api;
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
    public class PersonsController : BaseAPiController
    {
        private readonly IPersonsService  _personsService;

        public PersonsController(IPersonsService personsService)
        {
             _personsService = personsService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<List<PersonReadDto>>))]
        public async Task<IActionResult> Get([FromQuery]PersonSearchModel model)
        {
            var serviceResponse =  await _personsService.GetAll(model);

            if (serviceResponse.IsSuccess)
            {
                return Ok(serviceResponse.Result);
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(422, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> Create(PersonCreateDto personCreateDto)
        {
            var serviceResponse = await _personsService.Create(personCreateDto);

            if (serviceResponse.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = serviceResponse.Result.Id }, serviceResponse.Result); 
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResponse =  await _personsService.GetById(id);

            if (serviceResponse.IsSuccess)
            {
                return Ok(serviceResponse.Result);
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> Update(int id, PersonUpdateDto personUpdateDto)
        {
            var serviceResponse =  await _personsService.Update(id, personUpdateDto);

            if (serviceResponse.IsSuccess)
            {
                return Ok(serviceResponse.Result);
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpPut("{id}/phones/{phone-number-id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute] int id, [FromRoute(Name = "phone-number-id")] int phoneNumberId, PhoneNumberUpdateDto phoneNumberUpdateDto)
        {
            var serviceResponse =  await _personsService.UpdatePersonPhone(id, phoneNumberId, phoneNumberUpdateDto);

            if (serviceResponse.IsSuccess)
            {
                return Ok();
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpDelete("{id}/phones/{phone-number-id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> RemovePhoneNumber([FromRoute]int id, [FromRoute(Name = "phone-number-id")] int phoneNumberId)
        {
            var serviceResponse =  await _personsService.RemovePersonsPhone(id, phoneNumberId);

            if (serviceResponse.IsSuccess)
            {
                return NoContent();
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpPost("{id}/phones")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> AddPhonesForPerson([FromRoute]int id, PhoneNumberCreateDto phoneNumberCreateDto)
        {
            var serviceResponse =  await _personsService.AddPhoneNumber(id, phoneNumberCreateDto);

            if (serviceResponse.IsSuccess)
            {
                // todo: Created status should be here
                return Ok();
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]

        public async Task<IActionResult> Delete(int id)
        {
            var serviceResponse =  await _personsService.Delete(id);

            if (serviceResponse.IsSuccess)
            {
                return NoContent();
            }
            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpPost("{id}/connected-persons/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        [ProducesResponseType(409, Type = typeof(ServiceResponse))]
        [ProducesResponseType(422, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> AddConnectedPerson([FromRoute] int id, [FromBody]PersonConnectionsCreateDto relatedPersonCreateDto)
        {
            var serviceResponse =  await _personsService.CreateConnection(id, relatedPersonCreateDto);

            if (serviceResponse.IsSuccess)
            {
                return Ok();
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpPut("{id}/connected-persons/{connected-person-id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        [ProducesResponseType(422, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> UpdateConnectionType(
            [FromRoute] int id,
            [FromRoute(Name = "connected-person-id")] int targetPersonId,
            [FromBody] int relationShipId)
        {
            var serviceResponse =  await _personsService.UpdatePersonConnection(id, targetPersonId, relationShipId);

            if (serviceResponse.IsSuccess)
            {
                return Ok();
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }

        [HttpDelete("{id}/connected-persons/{connected-person-id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        public async Task<IActionResult> RemoveConnection([FromRoute]int id, [FromRoute(Name = "connected-person-id")] int targetPersonId)
        {
            var serviceResponse =  await _personsService.RemovePersonConnection(id, targetPersonId);

            if (serviceResponse.IsSuccess)
            {
                return NoContent();
            }

            return TransformServiceErrorToHttpStatusCode(serviceResponse.ServiceErrorMessage);
        }
    }
}
