namespace Rent_it.React.Server.Models.Klanten
{
    public class ZakelijkeKlant : Klant
    {
        /*private int bedrijfID;
        private string bedrijfsNaam;

        public void registerZakelijkeKlant(string bedrijfsNaam, string naam, string email, string wachtwoord)
        {
            base.Registreren(naam, email, wachtwoord);
            Geschiedenis.Add($"Zakelijke klant geregistreerd: {bedrijfsNaam} op {DateTime.Now}");
        }

        public void maakAbonnement(string type, decimal prijs, DateTime einddatum)
        {
            Abonnement = new Abonnement
            {
                type = type,
                prijs = prijs,
                einddatum = einddatum
            };
            Geschiedenis.Add($"Abonnement aangemaakt: {type}, geldig tot {einddatum}")
        }

        public void wijzigAbonnement(string nieuwType, decimal nieuwePrijs, DateTime nieuweEinddatum)
        {
            if (Abonnement == null) {
                throw new InvalidOperationException("Er is nog geen abonnement om te wijzigen.")
                    }
            Abonnement.type = nieuwType;
            Abonnement.prijs = nieuwePrijs;
            Abonnement.einddatum = nieuweEinddatum;
            Geschiedenis.Add($"Abonnement gewijzigd naar: {nieuwType}, geldig tot {nieuweEinddatum}.")
        }

        public void voegMedewerkerToe(string medewerkerNaam, string bedrijfsNaam)
        {
            Medewerkers.Add(medewerkerNaam);
            Geschiedenis.Add($"Medewerker toegevoegd: {medewerkerNaam} op {DateTime.Now} aan bedrijf {bedrijfsNaam}")
        }

        public void limiteerAantalVoertuigen(int nieuwLimiet)
        {
            if (nieuwLimiet < 1) 
            {
                throw new ArgumentException("Het limiet moet minimaal 1 zijn.");
            }
            voertuigenLimiet = nieuwLimiet;
            Geschiedenis.Add($"Voertuigenlimiet gewijzigd naar: {voertuigenLimiet}.");
        }*/
    }
}
