using AutoMapper;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;

namespace TBCPersonsDirectory.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PersonCreateDto, Person>()
                .ForMember(c => c.GenderId, d => d.MapFrom(x => x.GenderId))
                .ForMember(c => c.CityId, d => d.MapFrom(x => x.CityId));

            CreateMap<Person, PersonReadDto>()
                .ForMember(c => c.City, d => d.MapFrom(x => x.City.Name))
                .ForMember(c => c.Gender, d => d.MapFrom(x => x.Gender.Name));

            CreateMap<PersonUpdateDto, Person>()
                .ForMember(c => c.GenderId, d => d.MapFrom(x => x.GenderId))
                .ForMember(c => c.CityId, d => d.MapFrom(x => x.CityId));

            CreateMap<PersonPhoneNumber, PhoneNumberReadDto>()
              .ForMember(c => c.Type, d => d.MapFrom(x => x.PhoneNumberType.Name))
              .ForMember(c => c.Number, d => d.MapFrom(x => x.Number));

            CreateMap<PhoneNumberCreateDto, PersonPhoneNumber>();
            CreateMap<PhoneNumberUpdateDto, PersonPhoneNumber>();

            CreateMap<PersonConnection, PersonConnectionsReadDto>()
                 .ForMember(c => c.Person, d => d.MapFrom(x => x.SecondPerson));

            CreateMap<PersonConnectionsCreateDto, PersonConnection>()
                  .ForMember(c => c.ConnectionTypeId, d => d.MapFrom(x => x.ConnectionTypeId))
                  .ForMember(c => c.SecondPersonId, d => d.MapFrom(x => x.TargetPersonId));
            ;
        }
    }
}
