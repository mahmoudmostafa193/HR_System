using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRSystem.DTO.approval
{
    public class UpdateApprovalDTO
    {
        [JsonIgnore]
        public DateTime? from { get; set; }
        [JsonIgnore]
        public DateTime? to { get; set; } = DateTime.Now;
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

    
        public string? Status { get; set; }

        [JsonIgnore]
        public DateTime? ApprovalDate { get; set; }
        [JsonIgnore]

        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
