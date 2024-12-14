using Microsoft.EntityFrameworkCore;
using Rent_it.React.Server.Models.Bedrijven;
using Rent_it.React.Server.Models.Klanten;
using Rent_it.React.Server.Models.RentIt;

namespace Rent_it.React.Server.Data
{
    public class RentItDbContext : DbContext
    {
        public RentItDbContext(DbContextOptions<RentItDbContext> options) : base(options) { }
        
        public DbSet<Voertuig> Voertuigen { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Abonnement> Abonnementen { get; set; }
    }
}
