using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Core;

namespace TBCPersonsDirectory.Repository.Interfaces
{
    public interface IPersonsRepository : IBaseRepository<Person, int>
    {
    }
}
