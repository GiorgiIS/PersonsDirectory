using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;
using TBCPersonsDirectory.Services.Models;

namespace TBCPersonsDirectory.Services.Interfaces
{
    public interface IPersonsService
    {
        ServiceResponse<List<PersonReadDto>> GetAll(PersonSearchModel model);
        ServiceResponse Create(PersonCreateDto personCreateDto);
        ServiceResponse<PersonReadDto> GetById(int id);
        bool Exists(int id);
        ServiceResponse<int> Update(int id, PersonUpdateDto personUpdateDto);
        ServiceResponse Delete(int id);
        bool PersonHasPhone(int personId, int phoneId);
        ServiceResponse UpdatePersonPhone(int personId, int phoneId, PhoneNumberUpdateDto phoneNumberUpdateDto);
        ServiceResponse RemovePersonsPhone(int personId, int phoneId);
        ServiceResponse AddPhoneNumber(int personId, PhoneNumberCreateDto phoneNumberCreateDto);
        bool PersonConnectionTypeIsValid(int connectionTypeId);
        bool PersonHasConnection(int sourcePersonId, int targetPersonId);
        ServiceResponse CreateConnection(int sourcePersonId, PersonConnectionsCreateDto personConnectionsCreateDto);
        ServiceResponse UpdatePersonConnection(int sourcePersonId, int targetPersonId, int connectionTypeId);
        ServiceResponse RemovePersonConnection(int sourcePersonId, int targetPersonId);
    }
}
