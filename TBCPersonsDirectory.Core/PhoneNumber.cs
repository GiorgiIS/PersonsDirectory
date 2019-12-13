using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Core
{
    public class PhoneNumber : BaseEntity<int>
    {
        public string Number { get; set; }
        public PhoneNumberType? Type { get; set; } 
    }

    public enum PhoneNumberType
    {
        Office,
        Mobile,
        Home
    }
}
