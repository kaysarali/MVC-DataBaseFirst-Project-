using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaysar_Mvc_DataBaseFirst_Project.Repository
{
    public class CustomValidationDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime CurrentDate = DateTime.Now;
            string message = string.Empty;

            if (Convert.ToDateTime(value) > CurrentDate)
            {
                message = "Reduce Date of Birth";
                return new ValidationResult(message);
            }
            return ValidationResult.Success;
        }
    }
}