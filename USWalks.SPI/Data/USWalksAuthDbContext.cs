using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace USWalks.SPI.Data
{
    public class USWalksAuthDbContext:IdentityDbContext
    {
        public USWalksAuthDbContext(DbContextOptions<USWalksAuthDbContext> options) :base(options) 
        {
                
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "112c3956-24b2-4dd3-b421-f544ad5c855f";
            var writerRoleId = "ec74eca9-947b-4bc3-a9d1-ef82e53df1d2";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id =readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                 new IdentityRole
                {
                    Id =writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }      

    }
}
