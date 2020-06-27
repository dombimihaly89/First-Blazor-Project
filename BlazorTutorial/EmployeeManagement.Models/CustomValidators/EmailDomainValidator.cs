using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeManagement.Models.CustomValidators
{
    public class EmailDomainValidator : ValidationAttribute
    {

        private string DomainExtension { get; set; }
        private string ErrorMessage { get; set; }

        public EmailDomainValidator(string DomainExtension, string ErrorMessage)
        {
            this.DomainExtension = DomainExtension;
            this.ErrorMessage = ErrorMessage;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] parts = value.ToString().Split('.');
            if (parts[parts.Length - 1].ToUpper() == DomainExtension.ToUpper())
            {
                return null;
            }
            return new ValidationResult((ErrorMessage == null) ? $"Domain has to end with \"{DomainExtension}\"." : ErrorMessage, new[] { validationContext.MemberName });
        }
    }
}
