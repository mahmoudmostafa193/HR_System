
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HRSystem.Models
{
    public class Salaries
    {
        [Key]   
        public int SalaryId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public decimal WorkingStandard { get; set; } = 8;
        public decimal WorkingActual { get; set; }
        public decimal TotalSalary { get; set; }


        [RegularExpression("^(paid|Unpaid)$", ErrorMessage = "Status must be 'paid' or 'Unpaid'.")]
        public string Status { get; set; } = "paid";


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //[JsonIgnore]
        public Employee Employee { get; set; }
    }
}
