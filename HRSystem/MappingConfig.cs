using AutoMapper;
using HRSystem.Models;
using HRSystem.DTO.Employee;
using HRSystem.DTO.Salary;
using HRSystem.DTO.Attendance;
using HRSystem.DTO.approval;

namespace HRSystem
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
            CreateMap<Employee, GetEmployeeDto>().ReverseMap();
            CreateMap<Salaries, CreateSalaryDTO>().ReverseMap();
            CreateMap<Salaries, GetSalary>().ReverseMap();
            CreateMap<Attendance, CreateAttendanceDTO>().ReverseMap();
            CreateMap<Attendance,GetAttendanceDTO>().ReverseMap();
            CreateMap<Attendance, UpdateAttendanceDTO>().ReverseMap();
            CreateMap<Approvals,CreateApprovalDTO>().ReverseMap();
            CreateMap<Approvals, UpdateApprovalDTO>().ReverseMap();
        }
    }
}
