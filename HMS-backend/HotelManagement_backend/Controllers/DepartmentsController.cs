using AutoMapper;
using HotelManagement_backend.DomainModels;
using HotelManagement_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement_backend.Controllers
{
    [ApiController]
   // [Authorize(Roles ="Owner")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository departmentsRepository;
        private readonly IMapper mapper;
        public DepartmentsController(IDepartmentRepository departmentsRepository,IMapper mapper)
        {
            this.departmentsRepository = departmentsRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            var departments = await departmentsRepository.GetDepartmentsAsync(); 
            return Ok(mapper.Map<List<Department>>(departments));
        }

        [HttpGet]
        [Route("[controller]/{departmentId:int}"),ActionName("GetDepartmentAsync")]
        public async Task<IActionResult> GetDepartmentAsync([FromRoute] int departmentId)
        {
            var department = await departmentsRepository.GetDepartmentAsync(departmentId);
            if(department == null) 
            {
                return NotFound();
            }
            return Ok(mapper.Map<Department>(department));
        }

        [HttpPut]
        [Route("[controller]/{departmentId:int}")]
        public async Task<IActionResult> UpdateDepartmentAsync([FromRoute] int departmentId, [FromBody] UpdateDepartmentRequest request)
        {

            if (await departmentsRepository.Exists(departmentId))
            {
                var updatedguest = await departmentsRepository.UpdateDepartmentAsync(departmentId, mapper.Map<DataModels.Department>(request));
                if (updatedguest != null)
                {
                    return Ok(mapper.Map<Department>(updatedguest));
                }
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[controller]/{departmentId:int}")]
        public async Task<IActionResult> DeleteDepartmentAsync([FromRoute]int departmentId)
        {
            if (await departmentsRepository.Exists(departmentId))
            {
                var department = await departmentsRepository.DeleteDepartmentAsync(departmentId);
                return Ok(mapper.Map<Department>(department));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddDepartmentAsync([FromBody] AddDepartmentRequest request)
        {
            var department = await departmentsRepository.AddDepartmentAsync(mapper.Map<DataModels.Department>(request));
            return CreatedAtAction(nameof(GetDepartmentAsync), new { departmentId = department.DepartmentId },
                mapper.Map<Department>(department));
        }
    }
}
