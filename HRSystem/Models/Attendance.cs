using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }= DateTime.Now;

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public int? WorkingHours
        {
            get
            {
                if (CheckIn.HasValue && CheckOut.HasValue)
                {
                    return (int)(CheckOut.Value - CheckIn.Value).TotalHours;
                }
                return null;
            }
        }
        [RegularExpression("^(In-Site|Remote)$", ErrorMessage = "Status must be 'In-Site', or 'Remote'.")]
        public string Status { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Employee? Employee { get; set; }

    }
}
