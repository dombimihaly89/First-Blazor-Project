using EmployeeManagement.Models;
using EmployeeManagement.Models.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Models
{
    public class EditEmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Firstname must be provided")]
        [MinLength(2, ErrorMessage = "The name must be at least two characters")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [EmailDomainValidator("com", "The email address has to end with \"com\"")]
        public string Email { get; set; }
        [CompareProperty("Email", ErrorMessage = "E-mail fields are not matching")]
        public string ConfirmEmail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }
        [ValidateComplexType]
        public Department Department { get; set; } = new Department();
    }
}
