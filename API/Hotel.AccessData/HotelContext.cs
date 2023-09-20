
using Microsoft.EntityFrameworkCore;
using Obligatorio_1.Entidades;

namespace Hotel.AccessData
{
    public class HotelContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<CabinType> CabinTypes { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"SERVER=(localDb)\MsSqlLocalDb;DATABASE=Hotel_2023;Integrated Security=true; ENCRYPT=False").EnableDetailedErrors();
        }

    }
}