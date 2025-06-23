using HRSystem.Data;
using HRSystem.Models;
using HRSystem.Repositories.IRepositories;

namespace HRSystem.Repositories
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        private AppDbContext _context;
        public AttendanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Attendance> UpdateAsync(int id, Attendance updatedAttendance)
        {
            var existingAttendance = await _context.Attendances.FindAsync(id);
            if (existingAttendance == null)
            {
                return null;
            }

            
            existingAttendance.Status = updatedAttendance.Status;
            existingAttendance.CheckIn = updatedAttendance.CheckIn;
            existingAttendance.CheckOut = updatedAttendance.CheckOut;
            existingAttendance.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingAttendance;
        }

    }

}

