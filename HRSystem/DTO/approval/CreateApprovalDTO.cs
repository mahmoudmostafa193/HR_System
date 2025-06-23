using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRSystem.DTO.approval
{
    public class CreateApprovalDTO
    {
        [JsonIgnore]
        public DateTime? from { get; set; }=DateTime.Now;
        [JsonIgnore]
        public DateTime? to { get; set; } 
        public string Name { get; set; }
        [JsonIgnore]
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

        [JsonIgnore]

        public DateTime? ApprovalDate { get; set; } = DateTime.Now;
        [JsonIgnore]

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
