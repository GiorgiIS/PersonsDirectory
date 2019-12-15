using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TBCPersonsDirectory.Services.Dtos.CustomValidationAttributes;
using TBCPersonsDirectory.Services.Dtos.PhoneNumberDtos;

namespace TBCPersonsDirectory.Services.Dtos.PersonDtos
{
    [FirstNameAndLastNameShouldBeInSameLanguage]
    public class PersonCreateDto : PersonDtoInterface
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [ShouldBeOnlyGeorgianOrLatinLetters]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [ShouldBeOnlyGeorgianOrLatinLetters]
        public string LastName { get; set; }

        [Required]
        [StringLength(11)]
        public string PrivateNumber { get; set; }

        [Required]
        [AtLeast18YearsOld]
        public DateTime? BirthDate { get; set; }

        public int? GenderId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int? CityId { get; set; }

        public List<PhoneNumberCreateDto> PhoneNumbers { get; set; }
    }
}
