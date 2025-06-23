using HRSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Repositories.IRepositories
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        Task DeleteAsync(int id);
        Task<Employee> UpdateAsync(int id , Employee employee);
    }
}
