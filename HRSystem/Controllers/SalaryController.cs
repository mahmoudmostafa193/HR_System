using AutoMapper;
using HRSystem.DTO.Salary;
using HRSystem.Models;
using HRSystem.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace HRSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public SalaryController(ISalaryRepository salaryRepository, IEmployeeRepository employeeRepository,IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllSalries(int EmployeeId)
        {

            if (EmployeeId == null)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Employee not found" }
                });
            }
            var salaries = await _salaryRepository.GetAllAsync(filter: s=> s.EmployeeId==EmployeeId , includeProperties: "Employee");
            var model = _mapper.Map<List<GetSalary>>(salaries);

            if (salaries == null || salaries.Count == 0)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "No salaries found for this employee" }
                });
            }
            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true,
                Result = model
            });


        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> GetSalaryById(int id)
        {
            if(id == 0 || id == null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Salary id is null" }
                });
            }
            var salary = await _salaryRepository.GetByIdAsync(
           id,
           filter: s => s.SalaryId == id,
           includeProperties: "Employee");
            var model = _mapper.Map<GetSalary>(salary);
            if (salary == null)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Salary not found" }
                });
            }
            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true,
                Result = model
            });
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateSalary([FromQuery] int EmpId,[FromBody] CreateSalaryDTO salary)
        {
            if (salary == null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Salary is null" }
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

            var model = _mapper.Map<Salaries>(salary);
            model.EmployeeId = EmpId;
            var salaries = await _salaryRepository.AddAsync(model);


            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true,
                Result = model
            });
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> UpdateSalary(int id, [FromBody] CreateSalaryDTO salary)
        {
            if (salary == null || id == 0)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Salary is null" }
                });
            }
            var model = _mapper.Map <Salaries> (salary);
            await _salaryRepository.UpdateAsync(id, model);
            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true,
                Result = model
            });

        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> DeleteSalary(int id)
        {
            if(id == 0 || id==null)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Salary id is null" }
                });
            }
            await _salaryRepository.DeleteAsync(id);

            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true,
                Result = "Salary deleted successfully"
            });


        }
    }
}
