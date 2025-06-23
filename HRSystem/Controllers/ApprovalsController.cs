using AutoMapper;
using HRSystem.Data;
using HRSystem.DTO.approval;
using HRSystem.Models;
using HRSystem.Repositories;
using HRSystem.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace HRSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalsController : ControllerBase
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IApprovalsRepository _approvalsRepository;
        private readonly IMapper _mapper;

        public ApprovalsController(
            ISalaryRepository salaryRepository,
            IEmployeeRepository employeeRepository,
            IAttendanceRepository attendanceRepository,
            IApprovalsRepository approvalsRepository,
            IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _employeeRepository = employeeRepository;
            _attendanceRepository = attendanceRepository;
            _approvalsRepository = approvalsRepository;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet("GetApprovals")]
        public async Task<ActionResult<APIResponse>> GetApprovals()
        {
            var approvals = await _approvalsRepository.GetAllAsync();

            if (approvals == null || approvals.Count == 0)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "No approvals found" }
                });
            }
          

            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true,
                Result = approvals
            });
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateApproval(CreateApprovalDTO approval)
        {
            if (approval == null)
            {
                return BadRequest();
            }
            var model = _mapper.Map<Approvals>(approval);

            await _approvalsRepository.CreateAsync(model);
            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                IsSuccess = true,
                Result = model
            });

        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> updateApproval(int id, UpdateApprovalDTO approval)
        {
            if (approval == null)
            {
                return BadRequest();
            }

            var existingApproval = await _approvalsRepository.GetByIdAsync(id);
            if (existingApproval == null)
            {
                return NotFound(new APIResponse
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { "Approval not found" }
                });
            }

         
            _mapper.Map(approval, existingApproval);

          
            existingApproval.ApprovalDate ??= DateTime.Now;
            existingApproval.CreatedAt ??= DateTime.Now;
            existingApproval.UpdatedAt = DateTime.Now;

            var updatedApproval = await _approvalsRepository.UpdateAsync(existingApproval);

            return Ok(new APIResponse
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                IsSuccess = true,
                Result = updatedApproval
            });
        }







    }
}
