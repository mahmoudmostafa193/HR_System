using System.ComponentModel.DataAnnotations;

namespace HRSystem.DTO.Attendance
{
    public class CreateAttendanceDTO
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

  
 
    }
}
