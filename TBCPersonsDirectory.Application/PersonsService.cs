using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ServiceResponse AddPhoneNumber(int personId, PhoneNumberCreateDto phoneNumberCreateDto)
        {
            if (!Exists(personId))
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Person was not found"
                });
            }

            var person = _personsRepository.GetById(personId).Single();
            var phone = _mapper.Map<PersonPhoneNumber>(phoneNumberCreateDto);
            person.PhoneNumbers.Add(phone);
            _personsRepository.SaveChanges();

            return new ServiceResponse().Ok();
        }

        public ServiceResponse Create(PersonCreateDto personCreateDto)
        {
            var person = _mapper.Map<Person>(personCreateDto);

            _personsRepository.Create(person);
            _personsRepository.SaveChanges();

            return new ServiceResponse().Ok();
        }

        public ServiceResponse CreateConnection(int sourcePersonId, PersonConnectionsCreateDto personConnectionsCreateDto)
        {
            if (!Exists(sourcePersonId) || !Exists(personConnectionsCreateDto.TargetPersonId))
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
            _personConnectionsRepository.SaveChanges();

            return new ServiceResponse().Ok();
        }

        public ServiceResponse Delete(int id)
        {
            if (!Exists(id))
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Person was not found"
                });
            }

            _personsRepository.Delete(id);
            _personsRepository.SaveChanges();

            return new ServiceResponse().Ok();
        }

        public bool Exists(int id)
        {
            return _personsRepository.Exists(id);
        }

        public ServiceResponse<List<PersonReadDto>> GetAll(PersonSearchModel model)
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

        public ServiceResponse<PersonReadDto> GetById(int id)
        {
            if (!Exists(id))
                return new ServiceResponse<PersonReadDto>().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Person was not found"
                }); ;

            var person = _personsRepository.GetById(id)
                .Include(c => c.Gender)
                .Include(c => c.City)
                .Include(c => c.PhoneNumbers)
                .Include("PhoneNumbers.PhoneNumberType")
                .Single();

            person.PhoneNumbers = person.PhoneNumbers.Where(c => c.DeletedAt == null).ToList();

            var connectedPersons = _personConnectionsRepository.GetByPersonId(id).Include(c => c.SecondPerson);
            var notRemoved = connectedPersons.Where(c => c.SecondPerson.DeletedAt == null && c.DeletedAt == null);

            var connectedPersonsReadDtos = _mapper.Map<List<PersonConnectionsReadDto>>(notRemoved);

            var personDto = _mapper.Map<PersonReadDto>(person);
            personDto.ConnectedPersons.AddRange(connectedPersonsReadDtos);

            return new ServiceResponse<PersonReadDto>().Ok(personDto);
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

        public ServiceResponse RemovePersonConnection(int sourcePersonId, int targetPersonId)
        {
            if (!Exists(sourcePersonId) || !Exists(targetPersonId)
            || !PersonHasConnection(sourcePersonId, targetPersonId))
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Persons or target connection was not found"
                }); ;
            }

            _personConnectionsRepository.RemovePersonsConnection(sourcePersonId, targetPersonId);
            _personConnectionsRepository.SaveChanges();

            return new ServiceResponse().Ok();
        }

        public ServiceResponse RemovePersonsPhone(int personId, int phoneId)
        {
            if (!Exists(personId))
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Person was not found"
                });
            }

            if (!PersonHasPhone(personId, phoneId))
            {
                return new ServiceResponse().Fail(new ServiceErrorMessage
                {
                    Code = ErrorStatusCodes.NOT_FOUND,
                    Description = "Phone was not found"
                });
            }

            var phone = GetPersonsPhone(personId, phoneId);
            phone.DeletedAt = DateTime.UtcNow;
            phone.UpdatedAt = DateTime.UtcNow;

            _personsRepository.SaveChanges();

            return new ServiceResponse().Ok();
        }

        public ServiceResponse<int> Update(int id, PersonUpdateDto personUpdateDto)
        {
            var exists = Exists(id);

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
            _personsRepository.SaveChanges();

            return new ServiceResponse<int>().Ok(id);
        }

        public ServiceResponse UpdatePersonConnection(int sourcePersonId, int targetPersonId, int connectionTypeId)
        {
            if (!Exists(sourcePersonId) || !Exists(targetPersonId) || !PersonHasConnection(sourcePersonId, targetPersonId))
            {
                return new ServiceResponse<int>().Fail(
                    new ServiceErrorMessage()
                    {
                        Code = ErrorStatusCodes.NOT_FOUND,
                        Description = "sourcePersonId, targetPersonId or connectionTypeId was not found"
                    });
            }

            if (!PersonConnectionTypeIsValid(connectionTypeId))
            {
                return new ServiceResponse<int>().Fail(
                    new ServiceErrorMessage()
                    {
                        Code = ErrorStatusCodes.INVALID_VALUE,
                        Description = "relationShipId ahs invalid value"
                    });
            }

            _personConnectionsRepository.UpdatePersonConnection(sourcePersonId, targetPersonId, connectionTypeId);
            _personConnectionsRepository.SaveChanges();

            return new ServiceResponse().Ok();
        }

        public ServiceResponse UpdatePersonPhone(int personId, int phoneId, PhoneNumberUpdateDto phoneNumberUpdateDto)
        {
            if (!Exists(personId))
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

            _personsRepository.SaveChanges();

            return new ServiceResponse().Ok();
        }

        private PersonPhoneNumber GetPersonsPhone(int personId, int phoneId)
        {
            var person = _personsRepository.GetById(personId)
                .Include(c => c.PhoneNumbers).First();

            var phone = person.PhoneNumbers.FirstOrDefault(c => c.Id == phoneId && c.DeletedAt == null);
            return phone;
        }
    }
}
