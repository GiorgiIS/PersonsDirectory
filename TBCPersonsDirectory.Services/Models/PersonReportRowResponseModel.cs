using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Services.Models
{
    public class PersonReportRowResponseModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PrivateNumber { get; set; }
        public List<ConnectedPersonResponseModel> ConnectedPersons { get; set; } = new List<ConnectedPersonResponseModel>();
    }

    public class ConnectedPersonResponseModel
    {
        public int ConnectionTypeId { get; set; }
        public string ConnectionTypeName { get; set; }
        public int ConnectedPersonCount { get; set; }
    }

    public class PersonReportResponseModel
    {
        public List<PersonReportRowResponseModel> Rows { get; set; } = new List<PersonReportRowResponseModel>();
    }
}
