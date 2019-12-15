using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Core;

namespace TBCPersonsDirectory.Repository.Interfaces
{
    public interface IPersonConnectionsRepository : IBaseRepository<PersonConnection, int>
    {
        ConnectionType GetConnectionType(int connectionTypeId);
        PersonConnection GetConnection(int sourcePersonId, int targetPersonId);
        IQueryable<PersonConnection> GetByPersonId(int sourcePersonId);
        void RemovePersonsConnection(int sourcePersonId, int targetPersonId);
        void UpdatePersonConnection(int sourcePersonId, int targetPersonId, int connectionTypeId);
        List<ConnectionType> GetConnectionTypes();
    }
}
