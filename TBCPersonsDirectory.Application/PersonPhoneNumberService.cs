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
    public class PersonPhoneNumberService : IPersonPhoneNumberService
    {
        private readonly IPersonPhoneNumberRepository _personPhoneNumberRepository;
        private readonly IPersonsService _personsService;
        private readonly IMapper _mapper;

        public PersonPhoneNumberService(IPersonPhoneNumberRepository personPhoneNumberRepository, IPersonsService personsService, IMapper mapper)
        {
            _personPhoneNumberRepository = personPhoneNumberRepository;
            _personsService = personsService;
            _mapper = mapper;
        }

        public void Create(int personId, List<PhoneNumberCreateDto> phoneNumberCreateDtos)
        {
            var phoneNumbers = _mapper.Map<List<PersonPhoneNumber>>(phoneNumberCreateDtos);

            foreach (var number in phoneNumbers)
            {
                number.PersonId = personId;
                _personPhoneNumberRepository.Create(number);
            }

            _personPhoneNumberRepository.SaveChanges();
        }

        public void Delete(int personId, int id)
        {
            if (Exists(personId, id))
            {
                _personPhoneNumberRepository.Delete(id);
                _personPhoneNumberRepository.SaveChanges();
            }
        }

        public bool Exists(int personId, int phoneNumberId)
        {
            var phone = GetById(personId, phoneNumberId);
            return phone != null;
        }

        public List<PhoneNumberReadDto> GetAllByPersonId(int personId)
        {
            var phoneNumbers = _personPhoneNumberRepository.GetAll()
                .Include(c => c.PhoneNumberType)
                .Where(c => c.PersonId == personId);

            return _mapper.Map<List<PhoneNumberReadDto>>(phoneNumbers);
        }

        public PhoneNumberReadDto GetById(int personId, int id)
        {
            var phoneNumber = _personPhoneNumberRepository
                .GetById(id)
                .Include(c => c.PhoneNumberType)
                .Where(c => c.PersonId == personId)
                .FirstOrDefault();

            return _mapper.Map<PhoneNumberReadDto>(phoneNumber);
        }

        public void Update(int personId, int phoneId, PhoneNumberUpdateDto phoneNumberUpdateDto)
        {
            if (Exists(personId, phoneId))
            {
                var updateTarget = _mapper.Map<PersonPhoneNumber>(phoneNumberUpdateDto);
                updateTarget.PersonId = personId;
                updateTarget.Id = phoneId;

                _personPhoneNumberRepository.Update(updateTarget);
                _personPhoneNumberRepository.SaveChanges();
            }
        }
    }
}