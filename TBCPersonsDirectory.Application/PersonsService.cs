using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Repository.Interfaces;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Application
{
    public class PersonsService : IPersonsService
    {
        private readonly IPersonsRepository _personsRepository;

        public PersonsService(IPersonsRepository personsRepository)
        {
            _personsRepository = personsRepository;
        }

        public List<PersonReadDto> GetAll()
        {
            return _personsRepository.GetAll().Select(c => new PersonReadDto
            {
                BirthDate = c.BirthDate,
                FirstName = c.FirstName,
                City = c.City,
                ImageUrl = c.ImageUrl,
                LastName = c.LastName,
                PrivateNumber = c.PrivateNumber
            }).ToList();
        }
    }
}
