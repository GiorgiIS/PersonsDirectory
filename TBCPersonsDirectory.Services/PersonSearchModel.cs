using System;
using System.Collections.Generic;
using System.Text;
using TBCPersonsDirectory.Common;

namespace TBCPersonsDirectory.Services
{
    public class PersonSearchModel : BaseSearchModel<int>
    {
        public PaginationModel Pagination { get; set; } = new PaginationModel();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        public int? CityId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
