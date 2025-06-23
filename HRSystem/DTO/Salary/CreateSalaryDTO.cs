namespace HRSystem.DTO.Salary
{
    public class CreateSalaryDTO
    {

        //public int EmployeeId { get; set; }

        public decimal WorkingStandard { get; set; }
        public decimal WorkingActual { get; set; }
        public decimal TotalSalary { get; set; }

        public string Status { get; set; } = "Pending";


    }
}
