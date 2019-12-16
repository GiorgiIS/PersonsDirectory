using System;
using System.Collections.Generic;
using System.Linq;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Repository.EF;
using TBCPersonsDirectory.Repository.Interfaces;

namespace TBCPersonsDirectory.Repository.Implementation
{
    public class PersonConnectionsRepository : BaseRepository<PersonConnection, int>, IPersonConnectionsRepository
    {
        public PersonConnectionsRepository(PersonsDbContext context) : base(context)
        {

        }

        public IQueryable<PersonConnection> GetConnections(int sourcePersonId)
        {
            var persons = _context.PersonConnections.Where(c => c.DeletedAt == null && c.FirstPersonId == sourcePersonId);
            return persons;
        }

        public ConnectionType GetConnectionType(int connectionTypeId)
        {
            var connection = _context.ConnectionType.Find(connectionTypeId);
            return connection;
        }

        public PersonConnection GetConnection(int sourcePersonId, int targetPersonId)
        {
            var connection = _context.PersonConnections.FirstOrDefault(c => c.FirstPersonId == sourcePersonId && c.SecondPersonId == targetPersonId && c.DeletedAt == null);

            return connection;
        }

        public void RemovePersonsConnection(int sourcePersonId, int targetPersonId)
        {
            var connection = _context.PersonConnections.FirstOrDefault(c => c.FirstPersonId == sourcePersonId && c.SecondPersonId == targetPersonId && c.DeletedAt == null);

            connection.DeletedAt = DateTime.UtcNow;
            connection.UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePersonConnection(int sourcePersonId, int targetPersonId, int connectionTypeId)
        {

            var connection = _context.PersonConnections.FirstOrDefault(c => c.FirstPersonId == sourcePersonId && c.SecondPersonId == targetPersonId && c.DeletedAt == null);

            connection.ConnectionTypeId= connectionTypeId;
            connection.UpdatedAt = DateTime.UtcNow;
        }

        public List<ConnectionType> GetConnectionTypes()
        {
            return _context.ConnectionType.ToList();
        }
    }
}
