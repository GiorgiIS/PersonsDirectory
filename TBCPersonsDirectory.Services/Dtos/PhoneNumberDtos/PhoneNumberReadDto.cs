using System;
using System.Collections.Generic;
using System.Text;

namespace TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos
{
    public class PhoneNumberReadDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
