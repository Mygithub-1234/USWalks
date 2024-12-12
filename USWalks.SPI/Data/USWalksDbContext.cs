using Microsoft.EntityFrameworkCore;
using USWalks.SPI.Models.Domain;

namespace USWalks.SPI.Data
{
    public class USWalksDbContext: DbContext
    {
        public USWalksDbContext(DbContextOptions<USWalksDbContext> dbContextOptions):base(dbContextOptions) 
        {
            
        }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //seed data for Difficulties
            //Easy, Medium, High
            var difficulties = new List<Difficulty>()
            { new Difficulty()
            {
                Id =Guid.Parse("2b4cc99c-cd73-43bc-b8fc-0ce67ab4504a"),
                Name = "Easy"
            },
             new Difficulty()
             {
                 Id=Guid.Parse("157c896b-ca66-4348-9549-d3befda23367"),
                 Name ="Medium"

             },
             new Difficulty()
             {
                 Id =Guid.Parse("280de39a-f6e1-4788-a72f-a5b4771ab3b1"),
                 Name ="High"
             }
            };
            //seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id =Guid.Parse("b2f933b5-2e35-4264-bbeb-86b24f20ebee"),
                    Name="Seatle",
                    Code="ST",
                    RegionImageUrl=""
                },
                new Region()
                {
                    Id=Guid.Parse("c28493e0-a5b5-498b-ad13-64a4a020c601"),
                    Name="Washington DC",
                    Code="DC",
                    RegionImageUrl=""
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
