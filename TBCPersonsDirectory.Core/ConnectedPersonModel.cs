using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Core
{
    public class ConnectedPersonModel : BaseEntity<int>
    {
        public int ConnectedPersonId { get; set; }
        public Person ConnectedPerson { get; set; }
        public ConnectionType ConnectionType { get; set; }
    }

    public enum ConnectionType
    {
        College,
        Familiar,
        Relative,
        Other
    }
}
