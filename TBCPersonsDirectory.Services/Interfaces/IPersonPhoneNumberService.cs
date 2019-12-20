using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;

namespace TBCPersonsDirectory.Services.Interfaces
{
    public interface IPersonPhoneNumberService
    {
        List<PhoneNumberReadDto> GetAllByPersonId(int personId);
        void Create(int personId, List<PhoneNumberCreateDto> phoneNumberCreateDto);
        PhoneNumberReadDto GetById(int personId, int phoneNumberid);
        bool Exists(int personId, int id);
        void Update(int personId, int phoneId, PhoneNumberUpdateDto phoneNumberUpdateDto);
        void Delete(int personId, int id);
    }
}
