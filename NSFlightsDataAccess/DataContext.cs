using Microsoft.EntityFrameworkCore;
using NSFlightsBusiness.Entities;

namespace NSFlightsDataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Journey> Journeys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "name=SqlServerConnect",
                    b => b.MigrationsAssembly(
                        "NSFlightsAPI"
                        )
                    );
            }
        }
    }
}
