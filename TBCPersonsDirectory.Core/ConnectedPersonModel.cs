using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Core
{
    public class ConnectedPersonModel
    {
        public int Id { get; set; }
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
