using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Services.Models
{
    public class ConnectionReportModel
    {
        public int PersonId { get; set; }
        public string FullName { get; set; }
        public string PrivateNumber { get; set; }
        public int ConnectionTypeId { get; set; }
        public int TargetPersonId { get; set; }
    }
}
