using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;

namespace TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos
{
    public class PersonConnectionsReadDto
    {
        public PersonReadDto Person { get; set; }
        public int ConnectionTypeId { get; set; }
    }
}
