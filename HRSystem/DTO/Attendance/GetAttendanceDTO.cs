using HRSystem.DTO.Employee;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.DTO.Attendance
{
    public class GetAttendanceDTO
    {
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

        public GetEmployeeDto? Employee { get; set; }
    }
}
