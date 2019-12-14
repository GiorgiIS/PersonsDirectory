using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Repository.Interfaces;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
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

        public List<PersonReadDto> GetAll()
        {
            var personDtos = _mapper.Map<List<PersonReadDto>>(_personsRepository.GetAll().Include(c => c.City));
            return personDtos;
        }
    }
}
