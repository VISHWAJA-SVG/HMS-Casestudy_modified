using AutoMapper;
using HotelManagement_backend.DomainModels;
using HotelManagement_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HotelManagement_backend.Controllers
{
    [ApiController]
   // [Authorize(Roles = "Manager")]
    public class StaffController : Controller
    {
        private readonly IStaffRepository staffRepository;
        private readonly IMapper mapper;
        public StaffController(IStaffRepository staffRepository, IMapper mapper)
        {
            this.staffRepository = staffRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStaffAsync()
        {
            var staff = await staffRepository.GetStaffAsync();

            return Ok(mapper.Map<List<Staff>>(staff));
        }

        [HttpGet]
        [Route("[controller]/{staffId:int}"),ActionName("GetStaffByIdAsync")]
        public async Task<IActionResult> GetStaffByIdAsync([FromRoute]int staffId) 
        {
            var staff = await staffRepository.GetStaffByIdAsync(staffId);
            if (staff == null)
            {
                return NotFound();

            }
            return Ok(mapper.Map<Staff>(staff));
        }

        [HttpPut]
        [Route("[controller]/{staffId:int}")]
        public async Task<IActionResult> UpdateStaffAsync([FromRoute] int staffId, [FromBody] UpdateStaffRequest request)
        {
            if (await staffRepository.Exists(staffId))
            {
                var updatedstaff = await staffRepository.UpdateStaffAsync(staffId, mapper.Map<DataModels.Staff>(request));
                if (updatedstaff != null)
                {
                    return Ok(mapper.Map<Staff>(updatedstaff));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{staffId:int}")]
        public async Task<IActionResult> DeleteStaffAsync([FromRoute] int staffId)
        {
            if (await staffRepository.Exists(staffId))
            {
                var guest = await staffRepository.DeleteStaffAsync(staffId);
                return Ok(mapper.Map<Staff>(guest));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStaffAsync([FromBody] AddStaffRequest request)
        {
            var staff = await staffRepository.AddStaff(mapper.Map<DataModels.Staff>(request));
            return CreatedAtAction(nameof(GetStaffByIdAsync), new { staffId = staff.EmployeeId },
                mapper.Map<Staff>(staff));
        }
    }
}
