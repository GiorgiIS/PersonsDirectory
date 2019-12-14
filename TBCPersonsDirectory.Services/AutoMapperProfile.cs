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
            CreateMap<Person, PersonReadDto>();
        }
    }
}
