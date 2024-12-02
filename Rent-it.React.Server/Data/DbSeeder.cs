using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Rent_it.React.Server.Models.Bedrijven;
using Rent_it.React.Server.Models.Klanten;
using System;
using System.Linq;
using System.Numerics;

namespace Rent_it.React.Server.Data
{
    public static class DbSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new RentItDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<RentItDbContext>>()))
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                // Kijk of er al data in de Klanten tabel zitten
                if (!context.Klanten.Any(k => k.Email == "thijsje123@gmail.com"))
                {
                    // Voeg (mock)data toe aan de Klanten tabel
                    context.Klanten.AddRange(
                        new Klant
                        {
                            Naam = "Gerrit Bakker",
                            Adres = "Mijnenveld 12, Den Haag",
                            Email = "gerrit@gmail.com",
                            TelNr = "0648777123",
                            RijbewijsDocNr = 111222
                        },
                        new Klant
                        {
                            Naam = "Sander Reesema",
                            Adres = "Wilhelminastraat 13, Amsterdam",
                            Email = "sanderzelf@gmail.com",
                            TelNr = "0658788124",
                            RijbewijsDocNr = 333444
                        },
                        new Klant
                        {
                            Naam = "Sint Nicolaas",
                            Adres = "Manzana calle 23, Madrid",
                            Email = "oudeman@gmail.com",
                            TelNr = "0608987146",
                            RijbewijsDocNr = 555666
                        },
                        new Klant
                        {
                            Naam = "Thijs Linden",
                            Adres = "Grafstraat 3, Delft",
                            Email = "thijsje123@gmail.com",
                            TelNr = "0609987244",
                            RijbewijsDocNr = 555789
                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
