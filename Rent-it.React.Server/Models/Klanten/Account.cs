namespace Rent_it.React.Server.Models.Klanten
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        public bool IsActief { get; set; }
    }
}