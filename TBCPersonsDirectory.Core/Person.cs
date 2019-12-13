using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Core
{
    public class Person : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public List<ConnectedPersonModel> ConnectedPersons { get; set; } = new List<ConnectedPersonModel>();
    }

    public enum Gender
    {
        Male,
        Female
    }
}
