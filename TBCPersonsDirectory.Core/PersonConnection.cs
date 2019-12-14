using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Core
{
    public class PersonConnection : BaseEntity<int>
    {
        public PersonConnection() { }

        public PersonConnection(int id, int firstPersonId, int secondPersonId, int connectionTypeId)
        {
            Id = id;
            FirstPersonId = firstPersonId;
            SecondPersonId = secondPersonId;
            ConnectionTypeId = connectionTypeId;
        }

        public int? FirstPersonId { get; set; }
        public Person FirstPerson { get; set; }

        public int? SecondPersonId { get; set; }
        public Person SecondPerson { get; set; }

        public int? ConnectionTypeId { get; set; }
        public ConnectionType ConnectionType { get; set; }
    }
}
