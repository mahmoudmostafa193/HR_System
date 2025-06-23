using HRSystem.Models;

namespace HRSystem.Repositories.IRepositories
{
    public interface IAttendanceRepository:IGenericRepository<Attendance>
    {
        Task DeleteAsync(int id);
        Task<Attendance> UpdateAsync(int id, Attendance attendance);

    }
}
