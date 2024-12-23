using Rent_it.React.Server.Models.Klanten;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public DateTime Startdatum { get; set; } = DateTime.Now;

        // Foreign key
        public int AccountID { get; set; }

        [ForeignKey("AccountID")]
        public Account? ParentAccount { get; set; }
    }
}
