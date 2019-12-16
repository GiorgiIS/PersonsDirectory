using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        Task<ServiceResponse<List<PersonReadDto>>> GetAll(PersonSearchModel model);
        Task<ServiceResponse<PersonReadDto>> Create(PersonCreateDto personCreateDto);
        Task<ServiceResponse<PersonReadDto>> GetById(int id);
        Task<ServiceResponse<int>> Update(int id, PersonUpdateDto personUpdateDto);
        Task<ServiceResponse> Delete(int id);
        bool PersonHasPhone(int personId, int phoneId);
        Task<ServiceResponse> UpdatePersonPhone(int personId, int phoneId, PhoneNumberUpdateDto phoneNumberUpdateDto);
        Task<ServiceResponse> RemovePersonsPhone(int personId, int phoneId);
        Task<ServiceResponse> AddPhoneNumber(int personId, PhoneNumberCreateDto phoneNumberCreateDto);
        bool PersonConnectionTypeIsValid(int connectionTypeId);
        bool PersonHasConnection(int sourcePersonId, int targetPersonId);
        Task<ServiceResponse> CreateConnection(int sourcePersonId, PersonConnectionsCreateDto personConnectionsCreateDto);
        Task<ServiceResponse> UpdatePersonConnection(int sourcePersonId, int targetPersonId, int connectionTypeId);
        Task<ServiceResponse> RemovePersonConnection(int sourcePersonId, int targetPersonId);
    }
}
