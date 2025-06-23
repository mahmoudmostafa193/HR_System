using HRSystem.Models;

namespace HRSystem.Repositories.IRepositories
{
    public interface ISalaryRepository: IGenericRepository<Salaries>
    {
        Task DeleteAsync(int id);
        Task<Salaries> UpdateAsync(int id, Salaries salary);

    }
}
