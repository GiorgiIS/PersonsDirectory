using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Repository.EF;
using TBCPersonsDirectory.Services.Dtos;
using TBCPersonsDirectory.Services.Interfaces;

namespace TBCPersonsDirectory.Application
{
    public class CitysService : ICitysService
    {
        private readonly PersonsDbContext _personsDbContext;

        public CitysService(PersonsDbContext personsDbContext)
        {
            _personsDbContext = personsDbContext;
        }

        public List<CityReadDto> GetAll()
        {
            var citys = _personsDbContext.Citys.Select(c => new CityReadDto
            {
                Id = c.Id,
                Name = c.Name
            });

            return citys.ToList();
        }
    }
}
