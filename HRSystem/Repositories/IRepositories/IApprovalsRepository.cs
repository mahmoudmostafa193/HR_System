using HRSystem.Models;

namespace HRSystem.Repositories.IRepositories
{
    public interface IApprovalsRepository:IGenericRepository<Approvals>
    {
        Task DeleteAsync(int id);
        Task<Approvals> UpdateAsync( Approvals Approval);

        Task<Approvals> CreateAsync(Approvals Approvals);



    }
}
