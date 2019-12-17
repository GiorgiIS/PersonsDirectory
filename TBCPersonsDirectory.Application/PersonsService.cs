using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Repository.Interfaces;
using TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;
using TBCPersonsDirectory.Services.Interfaces;
using TBCPersonsDirectory.Services.Models;

namespace TBCPersonsDirectory.Application
{
    public class PersonsService : IPersonsService
    {
        private readonly IPersonsRepository _personsRepository;
        private readonly IPersonConnectionsRepository _personConnectionsRepository;
        private readonly IPictureService _pictureService;
        private readonly IMapper _mapper;

        public PersonsService(IPersonsRepository personsRepository, IPersonConnectionsRepository personConnectionsRepository,
            IPictureService pictureService, IMapper mapper)
        {
            _personsRepository = personsRepository;
            _personConnectionsRepository = personConnectionsRepository;
            _pictureService = pictureService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddPhoneNumber(int personId, PhoneNumberCreateDto phoneNumberCreateDto)
        {
            var person = _personsRepository.GetById(personId, nameof(Person.PhoneNumbers));

            if (person == null)
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(personId.ToString()));
            }

            var phone = _mapper.Map<PersonPhoneNumber>(phoneNumberCreateDto);
            person.PhoneNumbers.Add(phone);

            int saveResult = _personsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.AddPhoneNumber)));
            }
        }

        public async Task<ServiceResponse<PersonReadDto>> Create(PersonCreateDto personCreateDto)
        {
            var person = _mapper.Map<Person>(personCreateDto);

            _personsRepository.Create(person);

            int saveResult = _personsRepository.SaveChanges();

            if (saveResult > 0)
            {
                var personReadDto = _mapper.Map<PersonReadDto>(person);

                return new ServiceResponse<PersonReadDto>().Ok(personReadDto);
            }
            else
            {
                return new ServiceResponse<PersonReadDto>()
                    .Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.Create)));
            }
        }

        public async Task<ServiceResponse> CreateConnection(int sourcePersonId, PersonConnectionsCreateDto personConnectionsCreateDto)
        {
            if (!_personsRepository.Exists(sourcePersonId))
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(sourcePersonId.ToString()));

            if (!_personsRepository.Exists(personConnectionsCreateDto.TargetPersonId))
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(personConnectionsCreateDto.TargetPersonId.ToString()));


            if (sourcePersonId == personConnectionsCreateDto.TargetPersonId)
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .InvalidValue($"SourcePersonId: {sourcePersonId} and TargetPersonId {targetPersonId}");
            }

            if (!PersonConnectionTypeIsValid(personConnectionsCreateDto.ConnectionTypeId))
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .InvalidValue(personConnectionsCreateDto.ConnectionTypeId.ToString()));

            if (PersonHasConnection(sourcePersonId, personConnectionsCreateDto.TargetPersonId))
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .AlreadyExists($"connection between {sourcePersonId} and {personConnectionsCreateDto}"));

            var connection = _mapper.Map<PersonConnection>(personConnectionsCreateDto);
            connection.FirstPersonId = sourcePersonId;

            _personConnectionsRepository.Create(connection);
            int saveResult = _personConnectionsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.CreateConnection)));
            }
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            if (!_personsRepository.Exists(id))
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Person was not found"
                });
            }

            _personsRepository.Delete(id);

            int saveResult = _personsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.Delete)));
            }
        }

        public async Task<ServiceResponse<List<PersonReadDto>>> GetAll(PersonSearchModel model)
        {
            var persons = _personsRepository.GetAll()
                .Include(c => c.PhoneNumbers)
                .Where(c =>
                    (model.Id == 0 ? true : c.Id == model.Id)
                    && (string.IsNullOrEmpty(model.PrivateNumber) ? true : c.PrivateNumber.Contains(model.PrivateNumber))
                    && (model.CityId == null ? true : c.CityId == model.CityId)
                    && (model.GenderId == null ? true : c.GenderId == model.GenderId)
                    && (string.IsNullOrEmpty(model.FirstName) ? true : c.FirstName.Contains(model.FirstName))
                    && (string.IsNullOrEmpty(model.LastName) ? true : c.LastName.Contains(model.LastName))
                    && (string.IsNullOrEmpty(model.PhoneNumber) ? true : c.PhoneNumbers.Any(x => x.Number.Contains(model.PhoneNumber))
                    && (string.IsNullOrEmpty(model.LastName) ? true : c.LastName.Contains(model.LastName)))
                    && (model.BirthDateFrom == null ? true : c.BirthDate == null ? false : model.BirthDateFrom < c.BirthDate)
                    && (model.BirthDateTo == null ? true : c.BirthDate == null ? false : model.BirthDateTo > c.BirthDate)
                    && (model.CreatedFrom == null ? true : model.CreatedFrom < c.CreatedAt)
                    && (model.CreatedTo == null ? true : model.CreatedTo > c.CreatedAt)
                    && (model.UpdatedFrom == null ? true : model.UpdatedFrom < c.UpdatedAt)
                    && (model.UpdatedTo == null ? true : model.UpdatedTo > c.CreatedAt)
                )
                .Skip((model.Pagination.TargetPage - 1) * model.Pagination.CountPerPage)
                .Take(model.Pagination.CountPerPage)
                .Include(c => c.City)
                .Include(c => c.Gender);

            var personDto = _mapper.Map<List<PersonReadDto>>(persons);

            return new ServiceResponse<List<PersonReadDto>>().Ok(personDto);
        }

        public async Task<ServiceResponse<PersonReadDto>> GetById(int id)
        {
            var person = _personsRepository.GetById(id,
                nameof(Person.Gender),
                nameof(Person.City),
                nameof(Person.PhoneNumbers),
                $"{nameof(Person.PhoneNumbers)}.{nameof(PersonPhoneNumber.PhoneNumberType)}");

            if (person == null)
            {
                return new ServiceResponse<PersonReadDto>()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(id.ToString()));
            }

            person.PhoneNumbers = person.PhoneNumbers.Where(c => c.DeletedAt == null).ToList();
            var connectedPersons = _personConnectionsRepository.GetConnections(id).Include(c => c.SecondPerson);
            var notRemoved = connectedPersons.Where(c => c.SecondPerson.DeletedAt == null && c.DeletedAt == null);

            var connectedPersonsReadDtos = _mapper.Map<List<PersonConnectionsReadDto>>(notRemoved);

            var personDto = _mapper.Map<PersonReadDto>(person);
            personDto.ConnectedPersons.AddRange(connectedPersonsReadDtos);

            return new ServiceResponse<PersonReadDto>().Ok(personDto);
        }

        public async Task<ServiceResponse> RemovePersonConnection(int sourcePersonId, int targetPersonId)
        {
            if (!_personsRepository.Exists(sourcePersonId))
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(sourcePersonId.ToString()));
            }

            if (!_personsRepository.Exists(targetPersonId))
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(targetPersonId.ToString()));
            }

            if (!PersonHasConnection(sourcePersonId, targetPersonId))
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound($"Connection between {sourcePersonId} and {targetPersonId}"));
            }


            _personConnectionsRepository.RemovePersonsConnection(sourcePersonId, targetPersonId);
            int saveResult = _personConnectionsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse().
                    Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.RemovePersonConnection)));
            }
        }

        public async Task<ServiceResponse> RemovePersonsPhone(int personId, int phoneId)
        {
            var person = _personsRepository.GetById(personId, nameof(Person.PhoneNumbers));
            if (person == null)
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(personId.ToString()));
            }

            var phone = person.PhoneNumbers.FirstOrDefault(c => c.Id == phoneId && c.DeletedAt == null);
            if (phone == null)
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(phoneId.ToString()));
            }

            person.PhoneNumbers = person.PhoneNumbers.Where(x => x.Id != phoneId).ToList();

            int saveResult = _personsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.RemovePersonConnection)));
            }
        }

        public async Task<ServiceResponse<int>> Update(int id, PersonUpdateDto personUpdateDto)
        {
            var exists = _personsRepository.Exists(id);

            if (!exists)
            {
                return new ServiceResponse<int>()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(id.ToString()));
            }

            var updated = _mapper.Map<Person>(personUpdateDto);
            updated.Id = id;

            _personsRepository.Update(updated);
            int saveResult = _personsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse<int>().Ok(id);
            }
            else
            {
                return new ServiceResponse<int>()
                    .Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.Update)));
            }
        }

        public async Task<ServiceResponse> UpdatePersonConnection(int sourcePersonId, int targetPersonId, int connectionTypeId)
        {
            if (!_personsRepository.Exists(sourcePersonId))
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(sourcePersonId.ToString()));
            }

            if (!_personsRepository.Exists(targetPersonId))
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(targetPersonId.ToString()));
            }

            if (sourcePersonId == targetPersonId)
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .InvalidValue($"SourcePersonId: {sourcePersonId} and TargetPersonId {targetPersonId}");
            }

            if (!PersonHasConnection(sourcePersonId, targetPersonId))
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound($"Connection between {sourcePersonId} and {targetPersonId}"));
            }

            if (!PersonConnectionTypeIsValid(connectionTypeId))
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .InvalidValue($"ConnectionTypeId: {connectionTypeId.ToString()}"));
            }

            _personConnectionsRepository.UpdatePersonConnection(sourcePersonId, targetPersonId, connectionTypeId);

            int saveResult = _personConnectionsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.UpdatePersonConnection)));
            }
        }

        public async Task<ServiceResponse> UpdatePersonPhone(int personId, int phoneId, PhoneNumberUpdateDto phoneNumberUpdateDto)
        {
            if (!_personsRepository.Exists(personId))
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(personId.ToString()));
            }

            if (!PersonHasPhone(personId, phoneId))
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound($"Persons phone with id: {phoneId}"));
            }

            var phone = GetPersonsPhone(personId, phoneId);
            phone.Number = phoneNumberUpdateDto.Number;
            phone.PhoneNumberTypeId = phoneNumberUpdateDto.PhoneNumberTypeId;
            phone.UpdatedAt = DateTime.UtcNow;

            int saveResult = _personsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.UpdatePersonPhone)));
            }
        }

        public async Task<ServiceResponse<string>> UploadPicture(int id, IFormFile file)
        {
            var person = _personsRepository.GetById(id);

            if (file == null || file.Length < 1)
            {
                return new ServiceResponse<string>()
                    .Fail(new ServiceErrorMessage()
                    .InvalidValue("file"));
            }

            if (person == null)
            {
                return new ServiceResponse<string>()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(id.ToString()));
            }

            var path = await _pictureService.Upload(file, id.ToString(), $"Person_{id}_Image");
            return new ServiceResponse<string>().Ok(path);
        }

        public async Task<ServiceResponse> UpdatePersonImagePath(int id, string path)
        {
            var person = _personsRepository.GetById(id);

            if (person == null)
            {
                return new ServiceResponse()
                    .Fail(new ServiceErrorMessage()
                    .NotFound(id.ToString()));
            }

            person.ImageUrl = path;

            int saveResult = _personsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse<int>()
                    .Fail(new ServiceErrorMessage()
                    .ChangesNotSaved(nameof(PersonsService.UpdatePersonImagePath)));
            }
        }

        private PersonPhoneNumber GetPersonsPhone(int personId, int phoneId)
        {
            var person = _personsRepository.GetById(personId, nameof(Person.PhoneNumbers));

            var phone = person.PhoneNumbers.FirstOrDefault(c => c.Id == phoneId && c.DeletedAt == null);
            return phone;
        }

        public bool PersonConnectionTypeIsValid(int connectionTypeId)
        {
            return _personConnectionsRepository.GetConnectionType(connectionTypeId) != null;
        }

        public bool PersonHasConnection(int sourcePersonId, int targetPersonId)
        {
            return _personConnectionsRepository.GetConnection(sourcePersonId, targetPersonId) != null;
        }

        public bool PersonHasPhone(int personId, int phoneId)
        {
            var phone = GetPersonsPhone(personId, phoneId);
            return phone != null;
        }
    }
}
