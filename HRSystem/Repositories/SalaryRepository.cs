using HRSystem.Data;
using HRSystem.Models;
using HRSystem.Repositories.IRepositories;

namespace HRSystem.Repositories
{
    public class SalaryRepository : GenericRepository<Salaries>, ISalaryRepository
    {
        private AppDbContext _context;
        public SalaryRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            var Salary=await _context.Salaries.FindAsync(id);
            if (Salary != null)
            {
                _context.Salaries.Remove(Salary);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Salaries> UpdateAsync(int id, Salaries salary)
        {
            var existingSalary = await _context.Salaries.FindAsync(id);
            if (existingSalary==null)
            {
                return null;
            }
            existingSalary.WorkingStandard = salary.WorkingStandard;
            existingSalary.WorkingActual = salary.WorkingActual;
            existingSalary.TotalSalary = salary.TotalSalary;
            existingSalary.Status = salary.Status;
            existingSalary.EmployeeId = salary.EmployeeId;
            existingSalary.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return existingSalary;
        }
    }
}
