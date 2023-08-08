using Microsoft.EntityFrameworkCore;
namespace HotelManagement_backend.DataModels
{
    public class HotelDbContext:DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Staff> Staffs { get; set;}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Bill> Bill { get; set; }
        //public DbSet<Inventory> Inventories { get; set; }

    }
}
