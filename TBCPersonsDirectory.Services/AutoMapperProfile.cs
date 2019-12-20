using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;

namespace TBCPersonsDirectory.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PersonCreateDto, Person>();

            CreateMap<Person, PersonReadDto>()
                .ForMember(c => c.City, d => d.MapFrom(x => x.City.Name));

            CreateMap<PersonPhoneNumber, PhoneNumberDto>()
               .ForMember(c => c.Type, d => d.MapFrom(x => x.PhoneNumberType.Name))
               .ForMember(c => c.Number, d => d.MapFrom(x => x.Number));
        }
    }
}
