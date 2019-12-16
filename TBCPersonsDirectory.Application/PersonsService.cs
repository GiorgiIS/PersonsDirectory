using AutoMapper;
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
        private readonly IMapper _mapper;

        public PersonsService(IPersonsRepository personsRepository, IPersonConnectionsRepository personConnectionsRepository, IMapper mapper)
        {
            _personsRepository = personsRepository;
            _personConnectionsRepository = personConnectionsRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddPhoneNumber(int personId, PhoneNumberCreateDto phoneNumberCreateDto)
        {
            var person = _personsRepository.GetById(personId, nameof(Person.PhoneNumbers));
            if (person == null)
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Person was not found"
                });
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
                return new ServiceResponse().Fail(
                    new ServiceErrorMessage
                    {
                        Code = ErrorStatusCodes.CHANGES_NOT_SAVED,
                        Description = $"Changes were not saved in {nameof(PersonsService.AddPhoneNumber)}"
                    }); ;
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
                return new ServiceResponse<PersonReadDto>().Fail(
                    new ServiceErrorMessage
                    {
                        Code = ErrorStatusCodes.CHANGES_NOT_SAVED,
                        Description = $"Changes were not saved in {nameof(PersonsService.Create)}"
                    }); ;
            }
        }

        public async Task<ServiceResponse> CreateConnection(int sourcePersonId, PersonConnectionsCreateDto personConnectionsCreateDto)
        {
            if (!_personsRepository.Exists(sourcePersonId) || !_personsRepository.Exists(personConnectionsCreateDto.TargetPersonId))
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "One or more person was not found"
                });

            if (!PersonConnectionTypeIsValid(personConnectionsCreateDto.ConnectionTypeId))
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.INVALID_VALUE,
                    Description = "Connection Type Id was not found"
                });


            if (PersonHasConnection(sourcePersonId, personConnectionsCreateDto.TargetPersonId))
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.ALREADY_EXISTS,
                    Description = "Connection between them already exists"
                });

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
                return new ServiceResponse().Fail(
                    new ServiceErrorMessage
                    {
                        Code = ErrorStatusCodes.CHANGES_NOT_SAVED,
                        Description = $"Changes were not saved in {nameof(PersonsService.CreateConnection)}"
                    }); ;
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
                return new ServiceResponse().Fail(
                    new ServiceErrorMessage
                    {
                        Code = ErrorStatusCodes.CHANGES_NOT_SAVED,
                        Description = $"Changes were not saved in {nameof(PersonsService.Delete)}"
                    }); ;
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
                return new ServiceResponse<PersonReadDto>().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Person was not found"
                });
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
            if (!_personsRepository.Exists(sourcePersonId) || !_personsRepository.Exists(targetPersonId)
            || !PersonHasConnection(sourcePersonId, targetPersonId))
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Persons or target connection was not found"
                }); ;
            }

            _personConnectionsRepository.RemovePersonsConnection(sourcePersonId, targetPersonId);
            int saveResult = _personConnectionsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse().Fail(
                    new ServiceErrorMessage
                    {
                        Code = ErrorStatusCodes.CHANGES_NOT_SAVED,
                        Description = $"Changes were not saved in {nameof(PersonsService.RemovePersonConnection)}"
                    }); ;
            }
        }

        public async Task<ServiceResponse> RemovePersonsPhone(int personId, int phoneId)
        {
            var person = _personsRepository.GetById(personId, nameof(Person.PhoneNumbers));
            if (person == null)
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Person was not found"
                });
            }
            var phone = person.PhoneNumbers.FirstOrDefault(c => c.Id == phoneId && c.DeletedAt == null);
            if (phone == null)
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Phone was not found"
                });
            }

            person.PhoneNumbers = person.PhoneNumbers.Where(x => x.Id != phoneId).ToList();

            int saveResult = _personsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse().Fail(
                    new ServiceErrorMessage
                    {
                        Code = ErrorStatusCodes.CHANGES_NOT_SAVED,
                        Description = $"Changes were not saved in {nameof(PersonsService.RemovePersonConnection)}"
                    }); ;
            }
        }

        public async Task<ServiceResponse<int>> Update(int id, PersonUpdateDto personUpdateDto)
        {
            var exists = _personsRepository.Exists(id);

            if (!exists)
            {
                return new ServiceResponse<int>().Fail(
                    new ServiceErrorMessage()
                    {
                        Code = ErrorStatusCodes.NOT_FOUND,
                        Description = "Not found"
                    });
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
                return new ServiceResponse<int>().Fail(
                    new ServiceErrorMessage
                    {
                        Code = ErrorStatusCodes.CHANGES_NOT_SAVED,
                        Description = $"Changes were not saved in {nameof(PersonsService.Update)}"
                    }); ;
            }

        }

        public async Task<ServiceResponse> UpdatePersonConnection(int sourcePersonId, int targetPersonId, int connectionTypeId)
        {
            if (!_personsRepository.Exists(sourcePersonId) || !_personsRepository.Exists(targetPersonId) || !PersonHasConnection(sourcePersonId, targetPersonId))
            {
                return new ServiceResponse().Fail(
                    new ServiceErrorMessage()
                    {
                        Code = ErrorStatusCodes.NOT_FOUND,
                        Description = "sourcePersonId, targetPersonId or connectionTypeId was not found"
                    });
            }

            if (!PersonConnectionTypeIsValid(connectionTypeId))
            {
                return new ServiceResponse().Fail(
                    new ServiceErrorMessage()
                    {
                        Code = ErrorStatusCodes.INVALID_VALUE,
                        Description = "relationShipId ahs invalid value"
                    });
            }

            _personConnectionsRepository.UpdatePersonConnection(sourcePersonId, targetPersonId, connectionTypeId);

            int saveResult = _personConnectionsRepository.SaveChanges();

            if (saveResult > 0)
            {
                return new ServiceResponse().Ok();
            }
            else
            {
                return new ServiceResponse().Fail(
                    new ServiceErrorMessage
                    {
                        Code = ErrorStatusCodes.CHANGES_NOT_SAVED,
                        Description = $"Changes were not saved in {nameof(PersonsService.UpdatePersonConnection)}"
                    }); ;
            }
        }

        public async Task<ServiceResponse> UpdatePersonPhone(int personId, int phoneId, PhoneNumberUpdateDto phoneNumberUpdateDto)
        {
            if (!_personsRepository.Exists(personId))
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Person Id was not found"
                });
            }

            if (!PersonHasPhone(personId, phoneId))
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Persons phone with id was not found"
                });
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
                return new ServiceResponse().Fail(
                    new ServiceErrorMessage
                    {
                        Code = ErrorStatusCodes.CHANGES_NOT_SAVED,
                        Description = $"Changes were not saved in {nameof(PersonsService.UpdatePersonPhone)}"
                    }); ;
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
