using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;

namespace TBCPersonsDirectory.Services.Interfaces
{
    public interface IPersonsService
    {
        List<PersonReadDto> GetAll(PersonSearchModel model);
        void Create(PersonCreateDto personCreateDto);
        PersonReadDto GetById(int id);
        bool Exists(int id);
        void Update(int id, PersonUpdateDto personUpdateDto);
        void Delete(int id);
        bool PersonHasPhone(int personId, int phoneId);
        void UpdatePersonPhone(int personId, int phoneId, PhoneNumberUpdateDto phoneNumberUpdateDto);
        void RemovePersonsPhone(int personId, int phoneId);
        void AddPhoneNumber(int personId, PhoneNumberCreateDto phoneNumberCreateDto);
        bool PersonConnectionTypeIsValid(int connectionTypeId); 
        bool PersonHasConnection(int sourcePersonId, int targetPersonId);
        void CreateConnection(int sourcePersonId, PersonConnectionsCreateDto personConnectionsCreateDto);
        void UpdatePersonConnection(int sourcePersonId, int targetPersonId, int connectionTypeId);
        void RemovePersonConnection(int sourcePersonId, int targetPersonId);
    }
}
