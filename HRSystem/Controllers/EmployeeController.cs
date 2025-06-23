using AutoMapper;
using HRSystem.DTO.Employee;
using HRSystem.Models;
using HRSystem.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HRSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            if (employees == null || !employees.Any())
            {
                return NotFound(new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "No employees found." }
                });
            }

            var result = _mapper.Map<List<GetEmployeeDto>>(employees);

            return Ok(new APIResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = result
            });
        }
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, filter: s => s.EmployeeId == id);
            if (employee == null)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { $"No employee found with ID {id}." }
                });
            }

            var result = _mapper.Map<GetEmployeeDto>(employee);

            return Ok(new APIResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = result
            });
        }
        [Authorize]

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] CreateEmployeeDto employeeDto)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage)
                                                     .ToList()
                });
            }

            if (string.IsNullOrEmpty(employeeDto.Name) || employeeDto.Name == "string" ||
       string.IsNullOrEmpty(employeeDto.MobileNumber) || employeeDto.MobileNumber == "string" ||
       string.IsNullOrEmpty(employeeDto.Email) || employeeDto.Email == "string" ||
       string.IsNullOrEmpty(employeeDto.Position) || employeeDto.Position == "string" ||
       string.IsNullOrEmpty(employeeDto.Hometown) || employeeDto.Hometown == "string" ||
       string.IsNullOrEmpty(employeeDto.Address?.Country) || employeeDto.Address.Country == "string" ||
       string.IsNullOrEmpty(employeeDto.Address?.Street) || employeeDto.Address.Street == "string" ||
       string.IsNullOrEmpty(employeeDto.Address?.City) || employeeDto.Address.City == "string")
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Name and Mobile Number are required." }
                });
            }

           
            var employee = _mapper.Map<Employee>(employeeDto);

          
            await _employeeRepository.AddAsync(employee);

       
            var resultDto = _mapper.Map<GetEmployeeDto>(employee);

        
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, new APIResponse
            {
                StatusCode = HttpStatusCode.Created,
                IsSuccess = true,
                Result = resultDto
            });
        }
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateEmployee(int id, [FromBody] CreateEmployeeDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new APIResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                     .Select(e => e.ErrorMessage).ToList()
                });
            }

            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { $"Employee with ID {id} not found." }
                });
            }

            _mapper.Map(updateDto, employee);
            employee.UpdatedAt = DateTime.UtcNow;

            var updated = await _employeeRepository.UpdateAsync(id, employee);
            var resultDto = _mapper.Map<GetEmployeeDto>(updated);

            return Ok(new APIResponse
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = resultDto
            });
        }
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { $"No employee found with ID {id}." }
                });
            }

            await _employeeRepository.DeleteAsync(id);
            return Ok(new APIResponse
            {
                StatusCode = HttpStatusCode.NoContent,
                IsSuccess = true
            });
        }
    }
}
