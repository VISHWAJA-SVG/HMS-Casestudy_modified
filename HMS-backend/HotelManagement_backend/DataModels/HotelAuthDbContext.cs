using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement_backend.DataModels
{
    public class HotelAuthDbContext : IdentityDbContext
    {
        public HotelAuthDbContext(DbContextOptions<HotelAuthDbContext> options) : base(options)
        {
        }
       protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var ownerRoleId = "bc4699c9-5e34-4898-a4d6-b73f9731fb57";
            var managerRoleId = "ccad286d-a7e9-4eea-8dce-5160dac821d3";
            var receptionistRoleId = "54852c7c-68e3-49f8-821b-8c8e6559f5a3";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = ownerRoleId,
                    ConcurrencyStamp = ownerRoleId,
                    Name = "Owner",
                    NormalizedName = "Owner".ToUpper()
                },
                 new IdentityRole
                {
                    Id = managerRoleId,
                    ConcurrencyStamp = managerRoleId,
                    Name = "Manager",
                    NormalizedName = "Manager".ToUpper()
                },
                  new IdentityRole
                {
                    Id = receptionistRoleId,
                    ConcurrencyStamp = receptionistRoleId,
                    Name = "Receptionist",
                    NormalizedName = "Receptionist".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
       }
    }
}
