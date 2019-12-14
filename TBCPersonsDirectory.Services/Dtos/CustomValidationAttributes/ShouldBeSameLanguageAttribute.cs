using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;

namespace TBCPersonsDirectory.Services.Dtos.CustomValidationAttributes
{
    public class FirstNameAndLastNameShouldBeInSameLanguageAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var personCreateDto = value as PersonCreateDto;

            var eLetters = Helper.ENGLISH_LETTERS;
            var gLetters = Helper.GEORGIAN_LETTERS;

            var fn = personCreateDto.FirstName[0];
            var ln = personCreateDto.LastName[0];

            var isValid = (eLetters.Contains(fn) && eLetters.Contains(ln)) ||
                (gLetters.Contains(fn) && gLetters.Contains(ln));

            return isValid;
        }
    }
}
