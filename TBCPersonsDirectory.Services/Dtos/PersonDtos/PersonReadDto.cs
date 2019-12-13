using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Services.Dtos.PersonDtos
{
    public class PersonReadDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
    }
}
