using System.ComponentModel.DataAnnotations;

namespace Rent_it.React.Server.Models.Bedrijven
{
    public class Abonnement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Bedrijfsnaam { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string KvkNummer { get; set; }

        [Required]
        public string Abonnementsvorm { get; set; }

        public DateTime AbonnementDate { get; set; } = DateTime.Now;
    }
}
