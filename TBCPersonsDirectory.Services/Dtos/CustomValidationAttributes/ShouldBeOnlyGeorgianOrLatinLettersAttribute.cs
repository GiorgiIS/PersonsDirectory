using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TBCPersonsDirectory.Common;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;

namespace TBCPersonsDirectory.Services.Dtos.CustomValidationAttributes
{
    public class ShouldBeOnlyGeorgianOrLatinLettersAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var georgianLetters = LanguageLetters.GEORGIAN_LETTERS;
            var englishLetters = LanguageLetters.ENGLISH_LETTERS;

            var stringedValue = value.ToString();

            var hasGeorgian = false;
            var hasEnglish = false;

            foreach (var georgianLetter in georgianLetters)
            {
                if (stringedValue.Contains(georgianLetter))
                {
                    hasGeorgian = true;
                    break;
                }
            }

            foreach (var englishLetter in englishLetters)
            {
                if (stringedValue.Contains(englishLetter))
                {
                    hasEnglish = true;
                    break;
                }
            }

            var isValid = hasEnglish ^ hasGeorgian;

            if (isValid)
            {
                return ValidationResult.Success;
            }

            var validationResult = new ValidationResult("FirstName and LastName should be in same language", new[] { nameof(PersonCreateDto) });

            return validationResult;
        }
    }
}
