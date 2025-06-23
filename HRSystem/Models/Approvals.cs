using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models
{
    public class Approvals
    {
        [Key]
        public int ApprovalId { get; set; }      
        public string Name { get; set; }
        public DateTime ? from { get; set; }
        public DateTime? to { get; set; }
        public int Length
        {
            get
            {
                if (from.HasValue && to.HasValue)
                {
                    return (to.Value - from.Value).Days;
                }
                return 0; 
            }
        }

        [Required]
        public string Status { get; set; }

        public DateTime? ApprovalDate { get; set; } 

        public DateTime? CreatedAt { get; set; } 

        public DateTime? UpdatedAt { get; set; } 
    }
}
