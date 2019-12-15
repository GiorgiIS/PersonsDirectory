using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Repository.Interfaces;
using TBCPersonsDirectory.Services;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Application
{
    public class PersonsService : IPersonsService
    {
        private readonly IPersonsRepository _personsRepository;
        private readonly IMapper _mapper;

        public PersonsService(IPersonsRepository personsRepository, IMapper mapper)
        {
            _personsRepository = personsRepository;
            _mapper = mapper;
        }

        public void Create(PersonCreateDto personCreateDto)
        {
            var person = _mapper.Map<Person>(personCreateDto);

            _personsRepository.Create(person);
            _personsRepository.SaveChanges();
        }

        public void Delete(int id)
        {
            _personsRepository.Delete(id);
            _personsRepository.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _personsRepository.Exists(id);
        }

        public List<PersonReadDto> GetAll(PersonSearchModel model)
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

            var personDtos = _mapper.Map<List<PersonReadDto>>(persons);
            return personDtos;
        }

        public PersonReadDto GetById(int id)
        {
            if (!_personsRepository.Exists(id))
                return PersonReadDto.Null();

            var person = _personsRepository.GetById(id)
                .Include(c => c.Gender)
                .Include(c => c.City)
                .Single();

            var personDto = _mapper.Map<PersonReadDto>(person);

            return personDto;
        }

        public void Update(int id, PersonUpdateDto personUpdateDto)
        {
            var updated = _mapper.Map<Person>(personUpdateDto);
            updated.Id = id;

            _personsRepository.Update(updated);
            _personsRepository.SaveChanges();
        }
    }
}
