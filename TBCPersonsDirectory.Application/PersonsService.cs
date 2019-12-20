using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Repository.Interfaces;
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

        public List<PersonReadDto> GetAll()
        {
            var persons = _personsRepository.GetAll()
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
