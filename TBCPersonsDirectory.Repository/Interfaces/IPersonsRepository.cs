using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Repository.EF;
using TBCPersonsDirectory.Repository.Implementation;

namespace TBCPersonsDirectory.Repository.Interfaces
{
    public class PersonsRepository : BaseRepository<Person, int>, IPersonsRepository
    {
        public PersonsRepository(PersonsDbContext context) : base(context)
        {
        }
    }
}
