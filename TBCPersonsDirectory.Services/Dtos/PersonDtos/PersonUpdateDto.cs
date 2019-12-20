using System;
using System.ComponentModel.DataAnnotations;
using TBCPersonsDirectory.Services.Dtos.CustomValidationAttributes;

namespace TBCPersonsDirectory.Services.Dtos.PersonDtos
{
    [FirstNameAndLastNameShouldBeInSameLanguage]
    public class PersonUpdateDto : PersonDtoInterface
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

        public int? CityId { get; set; }
    }
}
