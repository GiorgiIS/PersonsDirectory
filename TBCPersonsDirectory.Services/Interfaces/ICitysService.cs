using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos;
using TBCPersonsDirectory.Services.Dtos.PersonConnectionsDtos;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;
using TBCPersonsDirectory.Services.Models;

namespace TBCPersonsDirectory.Services.Interfaces
{
    public interface ICitysService
    {
        List<CityReadDto> GetAll();
    }
}
