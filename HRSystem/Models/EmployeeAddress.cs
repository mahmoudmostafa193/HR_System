using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HRSystem.Models
{
    public class EmployeeAddress
    {
        [Key]
        [JsonIgnore]
        public int AddressId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        // Navigation property (optional if address used by multiple employees)
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
