using HotelManagement_backend.DataModels;

namespace HotelManagement_backend.Repositories
{
    public interface IStaffRepository
    {
        Task<List<Staff>> GetStaffAsync();
        Task<Staff> GetStaffByIdAsync(int staffId);
        Task<bool> Exists(int staffId);
        Task<Staff> UpdateStaffAsync(int staffId, Staff request);
        Task<Staff> DeleteStaffAsync(int staffId);
        Task<Staff> AddStaff(Staff request);
    }
}
