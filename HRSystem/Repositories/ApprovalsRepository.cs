using HRSystem.Data;
using HRSystem.Models;
using HRSystem.Repositories.IRepositories;
using System.Linq.Expressions;

namespace HRSystem.Repositories
{
    public class ApprovalsRepository : GenericRepository<Approvals>, IApprovalsRepository
    {
        private AppDbContext _context;
        public ApprovalsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Approvals> CreateAsync(Approvals Approvals)
        {
           await _context.Approvals.AddAsync(Approvals);
           await _context.SaveChangesAsync();
            return Approvals;
        }

        public async Task DeleteAsync(int id)
        {
            var approval=await _context.Approvals.FindAsync(id);
            if (approval != null)
            {
                _context.Approvals.Remove(approval);
                await _context.SaveChangesAsync();
            }
        }




        public async Task<Approvals> UpdateAsync(Approvals updatedApproval)
        {
            _context.Approvals.Update(updatedApproval);
            await _context.SaveChangesAsync();
            return updatedApproval;
        }

    }
}
