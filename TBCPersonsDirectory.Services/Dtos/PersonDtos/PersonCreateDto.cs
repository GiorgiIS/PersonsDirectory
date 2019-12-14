using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Services.Dtos.CustomValidationAttributes;

namespace TBCPersonsDirectory.Services.Dtos.PersonDtos
{
    [FirstNameAndLastNameShouldBeInSameLanguage]
    public class PersonCreateDto
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

        [Range(0, 1)]
        public int? Gender { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int? CityId { get; set; }
    }
}
