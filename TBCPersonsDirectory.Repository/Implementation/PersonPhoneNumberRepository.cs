using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Repository.EF;
using TBCPersonsDirectory.Repository.Interfaces;

namespace TBCPersonsDirectory.Repository.Implementation
{
    public class PersonPhoneNumberRepository : BaseRepository<PersonPhoneNumber, int>, IPersonPhoneNumberRepository
    {
        public PersonPhoneNumberRepository(PersonsDbContext context) : base(context)
        {
        }
    }
}
