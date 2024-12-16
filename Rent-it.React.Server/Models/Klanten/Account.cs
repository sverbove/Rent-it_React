using Rent_it.React.Server.Models.Bedrijven;
using System.ComponentModel.DataAnnotations;

namespace Rent_it.React.Server.Models.Klanten
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Rol { get; set; }
        public bool IsActief { get; set; }

        // For 'Medewerkers' accounts only
        public int? ParentAccountId { get; set; }
        public Account ParentAccount { get; set; }
        public ICollection<Account> Medewerkers { get; set; }

        public Abonnement Abonnement { get; set; }
    }
}