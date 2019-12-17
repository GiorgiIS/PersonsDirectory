using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Services.Dtos.PersonDtos
{
    public interface PersonDtoInterface
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
