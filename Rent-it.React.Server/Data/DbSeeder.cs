using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Rent_it.React.Server.Models.Bedrijven;
using Rent_it.React.Server.Models.Klanten;
using Rent_it.React.Server.Models.RentIt;
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
                // Verwijder de database
                context.Database.EnsureDeleted();

                // Maak de database opnieuw aan
                context.Database.Migrate();

                if (!context.Accounts.Any())
                {
                    context.Accounts.AddRange(
                        new Account
                        {
                            Gebruikersnaam = "Particulier",
                            Email = "test@gmail.com",
                            Wachtwoord = BCrypt.Net.BCrypt.HashPassword("Password123"),
                            Rol = "Particuliere Klant",
                            IsActief = true
                        },
                        new Account
                        {
                            Gebruikersnaam = "Zakelijk",
                            Email = "test@bedrijf.com",
                            Wachtwoord = BCrypt.Net.BCrypt.HashPassword("Password123"),
                            Rol = "Zakelijke Klant",
                            IsActief = true
                        }
                    );
                    context.SaveChanges();
                }

                // Voeg je data toe zoals je eerder hebt gedefinieerd
                if (!context.Voertuigen.Any(v => v.Kenteken == "AB-123-CD"))
                {
                    context.Voertuigen.AddRange(
                        new Voertuig { Soort = "Auto", Merk = "Toyota", Type = "Corolla", Kenteken = "AB-123-CD", Kleur = "Rood", Aanschafjaar = 2018, PrijsPerDag = 75.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Ford", Type = "Focus", Kenteken = "EF-456-GH", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 85.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Volkswagen", Type = "Golf", Kenteken = "IJ-789-KL", Kleur = "Zwart", Aanschafjaar = 2020, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Honda", Type = "Civic", Kenteken = "MN-012-OP", Kleur = "Wit", Aanschafjaar = 2017, PrijsPerDag = 80.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "BMW", Type = "3 Serie", Kenteken = "QR-345-ST", Kleur = "Grijs", Aanschafjaar = 2021, PrijsPerDag = 150.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Audi", Type = "A4", Kenteken = "UV-678-WX", Kleur = "Zilver", Aanschafjaar = 2016, PrijsPerDag = 120.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Mercedes", Type = "C-Klasse", Kenteken = "YZ-901-AB", Kleur = "Blauw", Aanschafjaar = 2022, PrijsPerDag = 160.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Nissan", Type = "Qashqai", Kenteken = "CD-234-EF", Kleur = "Groen", Aanschafjaar = 2015, PrijsPerDag = 70.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Peugeot", Type = "208", Kenteken = "GH-567-IJ", Kleur = "Rood", Aanschafjaar = 2021, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Renault", Type = "Clio", Kenteken = "KL-890-MN", Kleur = "Zwart", Aanschafjaar = 2018, PrijsPerDag = 65.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Hobby", Type = "De Luxe", Kenteken = "OP-123-QR", Kleur = "Wit", Aanschafjaar = 2017, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Fendt", Type = "Bianco", Kenteken = "ST-456-UV", Kleur = "Grijs", Aanschafjaar = 2018, PrijsPerDag = 110.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Knaus", Type = "Sport", Kenteken = "WX-789-YZ", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Dethleffs", Type = "Camper", Kenteken = "AB-012-CD", Kleur = "Groen", Aanschafjaar = 2016, PrijsPerDag = 130.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Adria", Type = "Altea", Kenteken = "EF-345-GH", Kleur = "Z ilver", Aanschafjaar = 2020, PrijsPerDag = 85.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Eriba", Type = "Touring", Kenteken = "IJ-678-KL", Kleur = "Rood", Aanschafjaar = 2015, PrijsPerDag = 75.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Tabbert", Type = "Puccini", Kenteken = "MN-901-OP", Kleur = "Zwart", Aanschafjaar = 2021, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Burstner", Type = "Premio", Kenteken = "QR-234-ST", Kleur = "Wit", Aanschafjaar = 2019, PrijsPerDag = 100.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "LMC", Type = "Musica", Kenteken = "UV-567-WX", Kleur = "Blauw", Aanschafjaar = 2018, PrijsPerDag = 80.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Sprite", Type = "Cruzer", Kenteken = "YZ-890-AB", Kleur = "Grijs", Aanschafjaar = 2022, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Volkswagen", Type = "California", Kenteken = "CD-123-EF", Kleur = "Rood", Aanschafjaar = 2018, PrijsPerDag = 110.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Mercedes", Type = "Marco Polo", Kenteken = "GH-456-IJ", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 150.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Ford", Type = "Nugget", Kenteken = "KL-789-MN", Kleur = "Zwart", Aanschafjaar = 2020, PrijsPerDag = 120.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Fiat", Type = "Ducato", Kenteken = "OP-012-QR", Kleur = "Wit", Aanschafjaar = 2017, PrijsPerDag = 70.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Citroen", Type = "Jumper", Kenteken = "ST-345-UV", Kleur = "Grijs", Aanschafjaar = 2021, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Peugeot", Type = "Boxer", Kenteken = "WX-678-YZ", Kleur = "Zilver", Aanschafjaar = 2016, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Renault", Type = "Master", Kenteken = "AB-901-CD", Kleur = "Blauw", Aanschafjaar = 2022, PrijsPerDag = 130.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Iveco", Type = "Daily", Kenteken = "EF-234-GH", Kleur = "Groen", Aanschafjaar = 2015, PrijsPerDag = 75.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Opel", Type = "Movano", Kenteken = "IJ-567-KL", Kleur = "Rood", Aanschafjaar = 2021, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Nissan", Type = "NV400", Kenteken = "MN-890-OP", Kleur = "Zwart", Aanschafjaar = 2018, PrijsPerDag = 80.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Kia", Type = "Sportage", Kenteken = "QR-123-ST", Kleur = "Zilver", Aanschafjaar = 2019, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Hyundai", Type = "Tucson", Kenteken = "UV-456-WX", Kleur = "Blauw", Aanschafjaar = 2020, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Skoda", Type = "Octavia", Kenteken = "YZ-789-AB", Kleur = "Groen", Aanschafjaar = 2017, PrijsPerDag = 75.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Mazda", Type = "3", Kenteken = "CD-012-EF", Kleur = "Wit", Aanschafjaar = 2018, PrijsPerDag = 80.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Subaru", Type = "Impreza", Kenteken = "GH-345-IJ", Kleur = "Grijs", Aanschafjaar = 2021, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Suzuki", Type = "Vitara", Kenteken = "KL-678-MN", Kleur = "Zwart", Aanschafjaar = 2019, PrijsPerDag = 85.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Volvo", Type = "XC60", Kenteken = "OP-901-QR", Kleur = "Zilver", Aanschafjaar = 2020, PrijsPerDag = 150.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Mitsubishi", Type = "Outlander", Kenteken = "ST-234-UV", Kleur = "Rood", Aanschafjaar = 2017, PrijsPerDag = 80.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Jeep", Type = "Wrangler", Kenteken = "WX-567-YZ", Kleur = "Groen", Aanschafjaar = 2022, PrijsPerDag = 160.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Land Rover", Type = "Defender", Kenteken = "YZ-890-AB", Kleur = "Blauw", Aanschafjaar = 2021, PrijsPerDag = 170.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Bailey", Type = "Unicorn", Kenteken = "CD-123-EF", Kleur = "Grijs", Aanschafjaar = 2018, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Lunar", Type = "Clubman", Kenteken = "GH-456-IJ", Kleur = "Zwart", Aanschafjaar = 2019, PrijsPerDag = 90.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Swift", Type = "Conqueror", Kenteken = "KL-789-MN", Kleur = "Wit", Aanschafjaar = 2020, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Elddis", Type = "Avante", Kenteken = "OP-012-QR", Kleur = "Zilver", Aanschafjaar = 2017, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Compass", Type = "Casita", Kenteken = "ST-345-UV", Kleur = "Blauw", Aanschafjaar = 2021, PrijsPerDag = 90.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Coachman", Type = "VIP", Kenteken = "WX-678-YZ", Kleur = "Groen", Aanschafjaar = 2016, PrijsPerDag = 110.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Buccaneer", Type = "Commodore", Kenteken = "AB-901-CD", Kleur = "Rood", Aanschafjaar = 2022, PrijsPerDag = 120.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Caravelair", Type = "Allegra", Kenteken = "EF-234-GH", Kleur = "Zwart", Aanschafjaar = 2015, PrijsPerDag = 75.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Sterckeman", Type = "Starlett", Kenteken = "IJ-567-KL", Kleur = "Wit", Aanschafjaar = 2021, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Tab", Type = "320", Kenteken = "MN-890-OP", Kleur = "Grijs", Aanschafjaar = 2018, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Volkswagen", Type = "Grand California", Kenteken = "QR-123-ST", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 100.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Mercedes", Type = "Sprinter", Kenteken = "UV-456-WX", Kleur = "Groen", Aanschafjaar = 2020, PrijsPerDag = 130.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Ford", Type = "Transit Custom", Kenteken = "YZ-789-AB", Kleur = "Rood", Aanschafjaar = 2017, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Fiat", Type = "Talento", Kenteken = "CD-012-EF", Kleur = "Zwart", Aanschafjaar = 2018, PrijsPerDag = 70.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Citroen", Type = "SpaceTourer", Kenteken = "GH-345-IJ", Kleur = "Wit", Aanschafjaar = 2021, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Peugeot", Type = "Traveller", Kenteken = "KL-678-MN", Kleur = "Grijs", Aanschafjaar = 2019, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Renault", Type = "Trafic", Kenteken = "OP-901-QR", Kleur = "Blauw", Aanschafjaar = 2020, PrijsPerDag = 100.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Iveco", Type = "Daily", Kenteken = "ST-234-UV", Kleur = "Groen", Aanschafjaar = 2017, PrijsPerDag = 80.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Opel", Type = "Vivaro", Kenteken = "WX-567-YZ", Kleur = "Rood", Aanschafjaar = 2022, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Nissan", Type = "Primastar", Kenteken = "YZ-890-AB", Kleur = "Zwart", Aanschafjaar = 2021, PrijsPerDag = 85.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Toyota", Type = "Yaris", Kenteken = "CD-123-EF", Kleur = "Wit", Aanschafjaar = 2019, PrijsPerDag = 75.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Ford", Type = "Kuga", Kenteken = "GH-456-IJ", Kleur = "Grijs", Aanschafjaar = 2020, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Volkswagen", Type = "Passat", Kenteken = "KL-789-MN", Kleur = "Blauw", Aanschafjaar = 2017, PrijsPerDag = 80.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Honda", Type = "Accord", Kenteken = "OP-012-QR", Kleur = "Groen", Aanschafjaar = 2018, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "BMW", Type = "X5", Kenteken = "ST-345-UV", Kleur = "Zilver", Aanschafjaar = 2021, PrijsPerDag = 150.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Audi", Type = "Q7", Kenteken = "WX-678-YZ", Kleur = "Zwart", Aanschafjaar = 2019, PrijsPerDag = 140.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Mercedes", Type = "GLC", Kenteken = "AB-901-CD", Kleur = "Wit", Aanschafjaar = 2020, PrijsPerDag = 160.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Nissan", Type = "Juke", Kenteken = "EF-234-GH", Kleur = "Grijs", Aanschafjaar = 2017, PrijsPerDag = 70.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Peugeot", Type = "308", Kenteken = "IJ-567-KL", Kleur = "Blauw", Aanschafjaar = 2022, PrijsPerDag = 95.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Renault", Type = "Megane", Kenteken = "MN-890-OP", Kleur = "Groen", Aanschafjaar = 2021, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Tabbert", Type = "Rossini", Kenteken = "QR-123-ST", Kleur = "Rood", Aanschafjaar = 2018, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Dethleffs", Type = "Beduin", Kenteken = "UV-456-WX", Kleur = "Zwart", Aanschafjaar = 2019, PrijsPerDag = 90.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Fendt", Type = "Tendenza", Kenteken = "YZ-789-AB", Kleur = "Wit", Aanschafjaar = 2020, PrijsPerDag = 110.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Knaus", Type = "Sudwind", Kenteken = "CD-012-EF", Kleur = "Zilver", Aanschafjaar = 2017, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Hobby", Type = "Excellent", Kenteken = "GH-345-IJ", Kleur = "Blauw", Aanschafjaar = 2021, PrijsPerDag = 100.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Adria", Type = "Action", Kenteken = "KL-678-MN", Kleur = "Groen", Aanschafjaar = 2016, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Eriba", Type = "Feeling", Kenteken = "OP-901-QR", Kleur = "Rood", Aanschafjaar = 2022, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Burstner", Type = "Averso", Kenteken = "ST-234-UV", Kleur = "Zwart", Aanschafjaar = 2015, PrijsPerDag = 75.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "LMC", Type = "Vivo", Kenteken = "WX-567-YZ", Kleur = "Wit", Aanschafjaar = 2021, PrijsPerDag = 80.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Sprite", Type = "Major", Kenteken = "YZ-890-AB", Kleur = "Grijs", Aanschafjaar = 2018, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Volkswagen", Type = "Multivan", Kenteken = "CD-123-EF", Kleur = "Zwart", Aanschafjaar = 2019, PrijsPerDag = 95.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Mercedes", Type = "Vito", Kenteken = "GH-456-IJ", Kleur = "Wit", Aanschafjaar = 2020, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Ford", Type = "Custom", Kenteken = "KL-789-MN", Kleur = "Grijs", Aanschafjaar = 2017, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Fiat", Type = "Scudo", Kenteken = "OP-012-QR", Kleur = "Blauw", Aanschafjaar = 2018, PrijsPerDag = 70.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Citroen", Type = "Berlingo", Kenteken = "ST-345-UV", Kleur = "Groen", Aanschafjaar = 2021, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Peugeot", Type = "Partner", Kenteken = "WX-678-YZ", Kleur = "Rood", Aanschafjaar = 2019, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Renault", Type = "Kangoo", Kenteken = "AB-901-CD", Kleur = "Zwart", Aanschafjaar = 2020, PrijsPerDag = 100.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Iveco", Type = "Eurocargo", Kenteken = "EF-234-GH", Kleur = "Wit", Aanschafjaar = 2017, PrijsPerDag = 75.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Opel", Type = "Combo", Kenteken = "IJ-567-KL", Kleur = "Grijs", Aanschafjaar = 2022, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Nissan", Type = "NV200", Kenteken = "MN-890-OP", Kleur = "Blauw", Aanschafjaar = 2021, PrijsPerDag = 85.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Kia", Type = "Picanto", Kenteken = "QR - 123 - ST", Kleur = "Groen", Aanschafjaar = 2018, PrijsPerDag = 70.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Hyundai", Type = "i30", Kenteken = "UV-456-WX", Kleur = "Rood", Aanschafjaar = 2019, PrijsPerDag = 80.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Skoda", Type = "Superb", Kenteken = "YZ-789-AB", Kleur = "Zwart", Aanschafjaar = 2020, PrijsPerDag = 90.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Mazda", Type = "6", Kenteken = "CD-012-EF", Kleur = "Wit", Aanschafjaar = 2017, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Subaru", Type = "Forester", Kenteken = "GH-345-IJ", Kleur = "Grijs", Aanschafjaar = 2021, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Suzuki", Type = "Swift", Kenteken = "KL-678-MN", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 80.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Volvo", Type = "XC90", Kenteken = "OP-901-QR", Kleur = "Groen", Aanschafjaar = 2020, PrijsPerDag = 150.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Mitsubishi", Type = "Eclipse Cross", Kenteken = "ST-234-UV", Kleur = "Rood", Aanschafjaar = 2017, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Auto", Merk = "Jeep", Type = "Renegade", Kenteken = "WX-567-YZ", Kleur = "Zwart", Aanschafjaar = 2022, PrijsPerDag = 160.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Auto", Merk = "Land Rover", Type = "Discovery", Kenteken = "YZ-890-AB", Kleur = "Zilver", Aanschafjaar = 2021, PrijsPerDag = 170.00m, Beschikbaar = true },

                        /* Campers */
                        new Voertuig { Soort = "Camper", Merk = "Volkswagen", Type = "California", Kenteken = "AB-123-CD", Kleur = "Rood", Aanschafjaar = 2018, PrijsPerDag = 120.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Mercedes", Type = "Marco Polo", Kenteken = "EF-456-GH", Kleur = "Zilver", Aanschafjaar = 2019, PrijsPerDag = 130.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Ford", Type = "Transit Custom", Kenteken = "IJ-789-KL", Kleur = "Blauw", Aanschafjaar = 2020, PrijsPerDag = 140.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Fiat", Type = "Ducato", Kenteken = "MN-012-OP", Kleur = "Wit", Aanschafjaar = 2017, PrijsPerDag = 110.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Citroën", Type = "Jumper", Kenteken = "QR-345-ST", Kleur = "Grijs", Aanschafjaar = 2021, PrijsPerDag = 100.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Peugeot", Type = "Boxer", Kenteken = "UV - 678 - WX", Kleur = "Zwart", Aanschafjaar = 2016, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Renault", Type = "Master", Kenteken = "YZ-901-AB", Kleur = "Groen", Aanschafjaar = 2022, PrijsPerDag = 130.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Nissan", Type = "NV400", Kenteken = "CD-234-EF", Kleur = "Blauw", Aanschafjaar = 2015, PrijsPerDag = 80.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Opel", Type = "Movano", Kenteken = "GH-567-IJ", Kleur = "Zilver", Aanschafjaar = 2021, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Iveco", Type = "Daily", Kenteken = "KL-890-MN", Kleur = "Rood", Aanschafjaar = 2018, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Volkswagen", Type = "Grand California", Kenteken = "OP-123-QR", Kleur = "Wit", Aanschafjaar = 2017, PrijsPerDag = 120.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Mercedes", Type = "Sprinter", Kenteken = "ST-456-UV", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 130.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Ford", Type = "Nugget", Kenteken = "WX-789-YZ", Kleur = "Zwart", Aanschafjaar = 2020, PrijsPerDag = 140.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Fiat", Type = "Talento", Kenteken = "AB-012-CD", Kleur = "Groen", Aanschafjaar = 2016, PrijsPerDag = 110.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Citroën", Type = "SpaceTourer", Kenteken = "EF-345-GH", Kleur = "Rood", Aanschafjaar = 2018, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Peugeot", Type = "Traveller", Kenteken = "IJ-678-KL", Kleur = "Zwart", Aanschafjaar = 2021, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Renault", Type = "Trafic", Kenteken = "MN-901-OP", Kleur = "Wit", Aanschafjaar = 2020, PrijsPerDag = 120.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Nissan", Type = "Primastar", Kenteken = "QR-234-ST", Kleur = "Zilver", Aanschafjaar = 2019, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Opel", Type = "Vivaro", Kenteken = "UV-567-WX", Kleur = "Grijs", Aanschafjaar = 2022, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Iveco", Type = "Eurocargo", Kenteken = "YZ-890-AB", Kleur = "Zwart", Aanschafjaar = 2017, PrijsPerDag = 80.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Volkswagen", Type = "Multivan", Kenteken = "CD-123-EF", Kleur = "Blauw", Aanschafjaar = 2018, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Mercedes", Type = "Vito", Kenteken = "GH-456-IJ", Kleur = "Groen", Aanschafjaar = 2020, PrijsPerDag = 110.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Ford", Type = "Kuga Camper", Kenteken = "KL-789-MN", Kleur = "Zilver", Aanschafjaar = 2017, PrijsPerDag = 120.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Fiat", Type = "Scudo", Kenteken = "OP-012-QR", Kleur = "Rood", Aanschafjaar = 2018, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Citroën", Type = "Berlingo", Kenteken = "ST-345-UV", Kleur = "Wit", Aanschafjaar = 2021, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Peugeot", Type = "Partner", Kenteken = "WX-678-YZ", Kleur = "Grijs", Aanschafjaar = 2019, PrijsPerDag = 95.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Renault", Type = "Kangoo", Kenteken = "AB-901-CD", Kleur = "Blauw", Aanschafjaar = 2020, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Iveco", Type = "Eurocargo", Kenteken = "EF-234-GH", Kleur = "Wit", Aanschafjaar = 2017, PrijsPerDag = 80.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Opel", Type = "Combo", Kenteken = "IJ-567-KL", Kleur = "Grijs", Aanschafjaar = 2022, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Nissan", Type = "NV200", Kenteken = "MN-890-OP", Kleur = "Blauw", Aanschafjaar = 2021, PrijsPerDag = 85.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Kia", Type = "Picanto", Kenteken = "QR-123-ST", Kleur = "Groen", Aanschafjaar = 2018, PrijsPerDag = 70.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Hyundai", Type = "i30", Kenteken = "UV-456-WX", Kleur = "Rood", Aanschafjaar = 2019, PrijsPerDag = 80.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Skoda", Type = "Superb", Kenteken = "YZ-789-AB", Kleur = "Zwart", Aanschafjaar = 2020, PrijsPerDag = 90.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Mazda", Type = "6", Kenteken = "CD-012-EF", Kleur = "Wit", Aanschafjaar = 2017, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Subaru", Type = "Forester", Kenteken = "GH-345-IJ", Kleur = "Grijs", Aanschafjaar = 2021, PrijsPerDag = 95.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Suzuki", Type = "Swift", Kenteken = "KL-678-MN", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 80.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Volvo", Type = "XC90", Kenteken = "OP-901-QR", Kleur = "Groen", Aanschafjaar = 2020, PrijsPerDag = 150.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Mitsubishi", Type = "Eclipse Cross", Kenteken = "ST-234-UV", Kleur = "Rood", Aanschafjaar = 2017, PrijsPerDag = 85.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Camper", Merk = "Jeep", Type = "Renegade", Kenteken = "WX-567-YZ", Kleur = "Zwart", Aanschafjaar = 2022, PrijsPerDag = 160.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Camper", Merk = "Land Rover", Type = "Discovery", Kenteken = "YZ-890-AB", Kleur = "Zilver", Aanschafjaar = 2021, PrijsPerDag = 170.00m, Beschikbaar = true },

                        /* Caravans */
                        new Voertuig { Soort = "Caravan", Merk = "Hobby", Type = "De Luxe", Kenteken = "AB-123-CD", Kleur = "Wit", Aanschafjaar = 2018, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Fendt", Type = "Bianco", Kenteken = "EF-456-GH", Kleur = "Grijs", Aanschafjaar = 2019, PrijsPerDag = 110.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Caravan", Merk = "Knaus", Type = "Sport", Kenteken = "IJ-789-KL", Kleur = "Blauw", Aanschafjaar = 2020, PrijsPerDag = 120.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Adria", Type = "Altea", Kenteken = "MN-012-OP", Kleur = "Zilver", Aanschafjaar = 2017, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Dethleffs", Type = "Camper", Kenteken = "QR-345-ST", Kleur = "Groen", Aanschafjaar = 2021, PrijsPerDag = 100.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Caravan", Merk = "Tabbert", Type = "Puccini", Kenteken = "UV-678-WX", Kleur = "Zwart", Aanschafjaar = 2016, PrijsPerDag = 110.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Burstner", Type = "Premio", Kenteken = "YZ-901-AB", Kleur = "Wit", Aanschafjaar = 2022, PrijsPerDag = 120.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "LMC", Type = "Musica", Kenteken = "CD-234-EF", Kleur = "Blauw", Aanschafjaar = 2015, PrijsPerDag = 80.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Caravan", Merk = "Sprite", Type = "Cruzer", Kenteken = "GH-567-IJ", Kleur = "Rood", Aanschafjaar = 2021, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Bailey", Type = "Unicorn", Kenteken = "KL-890-MN", Kleur = "Grijs", Aanschafjaar = 2018, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Lunar", Type = "Clubman", Kenteken = "OP-123-QR", Kleur = "Zilver", Aanschafjaar = 2017, PrijsPerDag = 90.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Caravan", Merk = "Swift", Type = "Conqueror", Kenteken = "ST-456-UV", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 110.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Compass", Type = "Casita", Kenteken = "WX-789-YZ", Kleur = "Groen", Aanschafjaar = 2020, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Coachman", Type = "VIP", Kenteken = "AB-012-CD", Kleur = "Rood", Aanschafjaar = 2016, PrijsPerDag = 120.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Caravan", Merk = "Buccaneer", Type = "Commodore", Kenteken = "EF-345-GH", Kleur = "Zwart", Aanschafjaar = 2018, PrijsPerDag = 130.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Caravelair", Type = "Allegra", Kenteken = "IJ-678-KL", Kleur = "Wit", Aanschafjaar = 2021, PrijsPerDag = 90.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Sterckeman", Type = "Starlett", Kenteken = "MN-901-OP", Kleur = "Zilver", Aanschafjaar = 2020, PrijsPerDag = 100.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Caravan", Merk = "Tab", Type = "320", Kenteken = "QR-234-ST", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 110.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Eriba", Type = "Touring", Kenteken = "UV-567-WX", Kleur = "Grijs", Aanschafjaar = 2022, PrijsPerDag = 120.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Adria", Type = "Action", Kenteken = "YZ-890-AB", Kleur = "Rood", Aanschafjaar = 2017, PrijsPerDag = 90.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Caravan", Merk = "Fendt", Type = "Tendenza", Kenteken = "CD-123-EF", Kleur = "Wit", Aanschafjaar = 2018, PrijsPerDag = 100.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Knaus", Type = "Sudwind", Kenteken = "GH-456-IJ", Kleur = "Groen", Aanschafjaar = 2020, PrijsPerDag = 110.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Hobby", Type = "Excellent", Kenteken = "KL-789-MN", Kleur = "Zwart", Aanschafjaar = 2017, PrijsPerDag = 120.00m, Beschikbaar = false },
                        new Voertuig { Soort = "Caravan", Merk = "Dethleffs", Type = "Beduin", Kenteken = "OP-012-QR", Kleur = "Blauw", Aanschafjaar = 2019, PrijsPerDag = 130.00m, Beschikbaar = true },
                        new Voertuig { Soort = "Caravan", Merk = "Burstner", Type = "Premio Life", Kenteken = "KL-789-MN", Kleur = "Rood", Aanschafjaar = 2020, PrijsPerDag = 140.00m, Beschikbaar = true }
                    );
                    context.SaveChanges();
                }
            }
        }


        /*private static void ClearSpecificTables(RentItDbContext context)
        {
            // Disable all foreign key constraints temporarily
            context.Database.ExecuteSqlRaw("EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");

            // Delete data and reset primary key identity columns
            context.Database.ExecuteSqlRaw("DELETE FROM dbo.Abonnementen; DBCC CHECKIDENT ('dbo.Abonnementen', RESEED, 0);");
            context.Database.ExecuteSqlRaw("DELETE FROM dbo.Accounts; DBCC CHECKIDENT ('dbo.Accounts', RESEED, 0);");

            // Re-enable foreign key constraints
            context.Database.ExecuteSqlRaw("EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'");
        }*/
    }
}
