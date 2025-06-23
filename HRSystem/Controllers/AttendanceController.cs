using AutoMapper;
using HRSystem.DTO.Attendance;
using HRSystem.DTO.Salary;
using HRSystem.Models;
using HRSystem.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HRSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {

        private readonly ISalaryRepository _salaryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IMapper _mapper;

        public AttendanceController(
            ISalaryRepository salaryRepository,
            IEmployeeRepository employeeRepository,
            IAttendanceRepository attendanceRepository,
            IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _employeeRepository = employeeRepository;
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet("Analysis")]
        public async Task<ActionResult<APIResponse>> GetAnalysis([FromQuery] DateTime date)
        {
            if (date == null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Date is required" }
                });
            }
                var allAttendances = await _attendanceRepository.GetAllAsync();
            var totalemp=allAttendances.Where(a => a.CheckIn!=null)
                .Select(a => a.EmployeeId)
                .Distinct()
                .Count();
            var emp_int_site = allAttendances.Where(a => a.CheckIn != null && a.Status == "In-Site")
                .Select(a => a.EmployeeId)
                .Distinct()
                .Count();
            var emp_remote = allAttendances.Where(a => a.CheckIn != null && a.Status == "Remote")
                 .Select(a => a.EmployeeId)
                .Distinct()
                .Count();


                return Ok(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = new
                    {
                        TotalEmployees = totalemp,
                        EmployeesInSite = emp_int_site,
                        EmployeesRemote = emp_remote
                    }
                });


            }




        [Authorize]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllAttendance([FromQuery] DateTime date)
        {
            var startDate = date.Date; 
            var endDate = startDate.AddDays(1); 

            var Attendances = await _attendanceRepository.GetAllAsync(
                a => a.Date >= startDate && a.Date < endDate
                ,includeProperties: "Employee"
            );

            if (Attendances == null || Attendances.Count == 0)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "No Attendances found for this day" }
                });
            }
           
            var model = _mapper.Map<List<GetAttendanceDTO>>(Attendances);
            
            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true,
                Result = model
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateAttendance(int EmpId, [FromBody] CreateAttendanceDTO createAttendanceDTO)
        {
            if (EmpId == 0 || EmpId == null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Employee ID is required" }
                });
            }

            if(createAttendanceDTO==null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Attendance data is required" }
                });
            }
            var employee = await _employeeRepository.GetByIdAsync(EmpId);

            if (employee == null)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Employee not found" }
                });
            }
            employee.Status = "attend";
            var model = _mapper.Map<Attendance>(createAttendanceDTO);
            model.EmployeeId = EmpId;
            model.CreatedAt = DateTime.Now;
            await _attendanceRepository.AddAsync(model);
            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true,
                Result = createAttendanceDTO
            });



        }


        [HttpPut]
        public async Task<ActionResult<APIResponse>> UpdateAttendance(int id, UpdateAttendanceDTO updatedAttendance)
        {
            if (updatedAttendance == null || id==0)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Attendance ID is required" }
                });
            }
            var model =_mapper.Map<Attendance>(updatedAttendance);
            var attendance=await _attendanceRepository.UpdateAsync(id, model);

            if (attendance == null)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Attendance not found" }
                });
            }
            return Ok(
                new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = _mapper.Map<GetAttendanceDTO>(attendance)
                });

        }


        [HttpDelete]
        public async Task<ActionResult<APIResponse>> DeleteAttendance(int attendanceid)
        {
            if(attendanceid==0 )
            {
                return BadRequest("attendanceid is zero or null");
            }
            var attendance=await _attendanceRepository.GetByIdAsync(attendanceid);
            if(attendance == null)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Attendance not found" }
                });
            }
            await _attendanceRepository.DeleteAsync(attendanceid);
            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                IsSuccess = true,
                Result = attendance
            });


        }



}



}
   

