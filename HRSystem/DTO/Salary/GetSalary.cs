using HRSystem.DTO.Employee;

namespace HRSystem.DTO.Salary
{
    public class GetSalary
    {
        public int EmployeeId { get; set; }

        public decimal WorkingStandard { get; set; }
        public decimal WorkingActual { get; set; }
        public decimal TotalSalary { get; set; }

        public string Status { get; set; }

        public GetEmployeeDto Employee { get; set; }
    }
}
