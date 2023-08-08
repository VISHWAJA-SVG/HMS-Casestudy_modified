using HotelManagement_backend.DataModels;
namespace HotelManagement_backend.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentAsync(int departmentId);
        Task<bool> Exists(int departmentId);
        Task<Department> UpdateDepartmentAsync(int departmentId,Department request);
        Task<Department> DeleteDepartmentAsync(int departmentId);
        Task<Department> AddDepartmentAsync(Department request);
    }
}
