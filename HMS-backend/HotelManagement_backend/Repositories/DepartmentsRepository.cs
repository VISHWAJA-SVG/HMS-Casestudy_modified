using HotelManagement_backend.DataModels;
using Microsoft.EntityFrameworkCore;
namespace HotelManagement_backend.Repositories
{
    public class DepartmentsRepository : IDepartmentRepository
    {
        private readonly HotelDbContext context;
        public DepartmentsRepository(HotelDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentAsync(int departmentId)
        {
            return await context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == departmentId);
        }

        public async Task<bool> Exists(int departmentId)
        {
            return await context.Departments.AnyAsync(x => x.DepartmentId == departmentId); 
        }

        public async Task<Department> UpdateDepartmentAsync(int departmentId, Department request)
        {
            var existingDepartment = await GetDepartmentAsync(departmentId);
            if (existingDepartment != null)
            {
                existingDepartment.DepartmentName = request.DepartmentName;
               

                await context.SaveChangesAsync();
                return existingDepartment;
            }
            return null;
        }

        public async Task<Department> DeleteDepartmentAsync(int departmentId)
        {
            var department = await GetDepartmentAsync(departmentId);
            if (department != null)
            {
                context.Departments.Remove(department);
                await context.SaveChangesAsync();
                return department;
            }
            return null;
        }

        public async Task<Department> AddDepartmentAsync(Department request)
        {
            var department = await context.Departments.AddAsync(request);
            await context.SaveChangesAsync();
            return department.Entity;
        }
    }
}
