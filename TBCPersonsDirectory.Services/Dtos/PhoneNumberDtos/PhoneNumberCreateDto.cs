using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos
{
    public class PhoneNumberCreateDto
    {
        public int PhoneNumberTypeId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
