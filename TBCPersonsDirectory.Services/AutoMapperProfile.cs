using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;

namespace TBCPersonsDirectory.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PersonCreateDto, Person>()
                .ForMember(c => c.GenderId, d => d.MapFrom(d => d.GenderId));

            CreateMap<Person, PersonReadDto>()
                .ForMember(c => c.City, d => d.MapFrom(x => x.City.Name))
                .ForMember(c => c.Gender, d => d.MapFrom(d => d.Gender.Name));

            CreateMap<PersonPhoneNumber, PhoneNumberReadDto>()
               .ForMember(c => c.Type, d => d.MapFrom(x => x.PhoneNumberType.Name))
               .ForMember(c => c.Number, d => d.MapFrom(x => x.Number));
        }
    }
}
