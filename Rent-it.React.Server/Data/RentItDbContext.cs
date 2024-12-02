using Microsoft.EntityFrameworkCore;

namespace Rent_it.React.Server.Data
{
    public class RentItDbContext : DbContext
    {
        public RentItDbContext(DbContextOptions<RentItDbContext> options) : base(options)
        {

        }

        public DbSet<Models.Klanten.Klant> Klanten { get; set; }
    }
}
