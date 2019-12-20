using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos
{
    public class PhoneNumberCreateDto
    {
        [Required]
        public int? PhoneNumberTypeId { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        [RegularExpression("[0-9]+")]
        public string Number { get; set; }
    }
}
