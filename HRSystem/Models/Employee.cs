using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public int AddressId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        [Phone]
        public string MobileNumber { get; set; } = string.Empty;

        // Foreign Key
       

        // Navigation property
        [ForeignKey("AddressId")]
        public EmployeeAddress Address { get; set; }

        public decimal salary { get; set; } = 0;

        [RegularExpression("^(intern|Employee)$", ErrorMessage = "Type must be 'intern', or 'Employee'.")]
        public string Type { get; set; } = string.Empty;

        [RegularExpression("^(full-time|part-time)$", ErrorMessage = "Type must be 'full-time', or 'part-time'.")]
        public string WorkType { get; set; } = string.Empty;

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [RegularExpression("^(Tester|Backend|Frontend|flutter|Ai|UI)$", ErrorMessage = "Type must be choose Track")]
        public string JobType { get; set; } = string.Empty;

        public string Hometown { get; set; } = string.Empty;
        [RegularExpression("^(absent|attend)$", ErrorMessage = "Type must be 'absent', or 'attend'.")]
        public string Status { get; set; } = "absent";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<Salaries> Salaries { get; set; } = new List<Salaries>();

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
