using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;

namespace TBCPersonsDirectory.Services.Dtos.PersonDtos
{
    public class PersonReadDto : PersonDtoInterface
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }

        public List<PersonConnectionsReadDto> ConnectedPersons { get; set; } = new List<PersonConnectionsReadDto>();
        public List<PhoneNumberReadDto> PhoneNumbers { get; set; } = new List<PhoneNumberReadDto>();
    }
}
