using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Services.Models
{
    public class PersonReportRowModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PrivateNumber { get; set; }
        public Dictionary<int, int> ConnectedPersons { get; set; } = new Dictionary<int, int>();
    }
}
