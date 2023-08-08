using HotelManagement_backend.DataModels;

using Microsoft.EntityFrameworkCore;

namespace HotelManagement_backend.Repositories
{
    public class StaffRepository:IStaffRepository
    {
        private readonly HotelDbContext context;
        public StaffRepository(HotelDbContext context)
        {
            this.context = context;
        }

        

        public async Task<List<Staff>> GetStaffAsync()
        {
            return await context.Staffs.ToListAsync();
        }

        public async Task<Staff> GetStaffByIdAsync(int staffId)
        {
            return await context.Staffs.FirstOrDefaultAsync(x => x.EmployeeId == staffId);
        }
        public Task<bool> Exists(int staffId)
        {
            return context.Staffs.AnyAsync(x => x.EmployeeId == staffId);
        }

        public async Task<Staff> UpdateStaffAsync(int staffId, Staff request)
        {
            var existingStaff = await GetStaffByIdAsync(staffId);
            if (existingStaff != null)
            {
                existingStaff.EmployeeName = request.EmployeeName;
                existingStaff.Age = request.Age;
                existingStaff.EmployeeAddress = request.EmployeeAddress;
                existingStaff.Salary = request.Salary;
                existingStaff.Designation=request.Designation;
                existingStaff.Email = request.Email;
                existingStaff.PhoneNumber = request.PhoneNumber;

                await context.SaveChangesAsync();
                return existingStaff;
            }
            return null;
        }

        public async Task<Staff> DeleteStaffAsync(int staffId)
        {
            var staff = await GetStaffByIdAsync(staffId);
            if (staff != null)
            {
                context.Staffs.Remove(staff);
                await context.SaveChangesAsync();
                return staff;
            }
            return null;
        }

        public async Task<Staff> AddStaff(Staff request)
        {
            var staff = await context.Staffs.AddAsync(request);
            await context.SaveChangesAsync();
            return staff.Entity;
        }
    }
}
