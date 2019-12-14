using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Core
{
    public class PersonPhoneNumber : BaseEntity<int>
    {
        public PersonPhoneNumber() { }
        public PersonPhoneNumber(int id, int personId, string number, int phoneNumberTypeId)
        {
            Number = number;
            PhoneNumberTypeId = phoneNumberTypeId;
            PersonId = PersonId;
            Id = id;
        }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public string Number { get; set; }

        public int? PhoneNumberTypeId { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; } 
    }
}
