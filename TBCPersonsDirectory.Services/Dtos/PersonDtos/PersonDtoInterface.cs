using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Services.Dtos.PersonDtos
{
    public interface PersonDtoInterface
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
