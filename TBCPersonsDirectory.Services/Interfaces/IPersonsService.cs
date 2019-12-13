using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;

namespace TBCPersonsDirectory.Services.Interfaces
{
    public interface IPersonsService
    {
        List<PersonReadDto> GetAll();
    }
}
