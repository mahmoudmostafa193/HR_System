using HRSystem.Data;
using HRSystem.Models;
using HRSystem.Repositories.IRepositories;
using HRSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    private readonly ISalaryRepository _salaryRepository;
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context, ISalaryRepository salaryRepository, IAttendanceRepository attendanceRepository)
        : base(context)
    {
        _context = context;
        _salaryRepository = salaryRepository;
        _attendanceRepository = attendanceRepository;
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            var salaries = _context.Salaries.Where(s => s.EmployeeId == employee.EmployeeId);
            var attendances = _context.Attendances.Where(a => a.EmployeeId == employee.EmployeeId);

            foreach (var salary in salaries)
            {
                await _salaryRepository.DeleteAsync(salary.SalaryId);
            }

            foreach (var attendance in attendances)
            {
                await _attendanceRepository.DeleteAsync(attendance.AttendanceId);
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Employee> UpdateAsync(int id, Employee updatedEmployee)
    {
        var existingEmployee = await _context.Employees.FindAsync(id);
        if (existingEmployee == null)
        {
            return null; 
        }
        existingEmployee.Name = updatedEmployee.Name;
        existingEmployee.Position = updatedEmployee.Position;
        existingEmployee.MobileNumber = updatedEmployee.MobileNumber;
        //existingEmployee.Status = updatedEmployee.Status;
        existingEmployee.UpdatedAt = DateTime.Now;
        //_context.Entry(existingEmployee).State = EntityState.Modified;
        _context.Employees.Update(existingEmployee);
        await _context.SaveChangesAsync();
        return existingEmployee; 
    }



}
