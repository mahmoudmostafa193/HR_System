using HRSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.DTO.Employee
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;
        [Phone]
        public string MobileNumber { get; set; } = string.Empty;
        public EmployeeAddress Address { get; set; }

        [RegularExpression("^(intern|Employee)$", ErrorMessage = "Type must be 'intern', or 'Employee'.")]
        public string Type { get; set; } = string.Empty;

        [RegularExpression("^(full-time|part-time)$", ErrorMessage = "Type must be 'full-time', or 'part-time'.")]
        public string WorkType { get; set; } = string.Empty;
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
        [RegularExpression("^(Tester|Backend|Frontend|flutter|Ai|UI)$", ErrorMessage = "Type must be choose Track")]
        public string JobType { get; set; } = string.Empty;
        //[RegularExpression("^(Active|full-time|part-time)$", ErrorMessage = "Type must be 'intern', 'full-time', or 'part-time'.")]
        //public string Status { get; set; } = "Active";
        public string Hometown { get; set; } = string.Empty;


    }
}
