using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Models.CustomValidators
{
    public class EmailDomainValidator : ValidationAttribute
    {

        public string DomainExtension { get; set; }

        public EmailDomainValidator(string DomainExtension, string ErrorMessage)
        {
            this.DomainExtension = DomainExtension;
            this.ErrorMessage = ErrorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string[] parts = value.ToString().Split('.');
                if (parts[parts.Length - 1].ToUpper() == DomainExtension.ToUpper())
                {
                    return null;
                }
            }
            return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
        }
    }
}
