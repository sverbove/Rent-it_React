namespace Rent_it.React.Server.Models.RentIt
{
    public class Verhuuraanvraag
    {
        private int verhuurID;
        private int klantID;
        private int voertuigID;
        private int verwachteKilometers;
        private int rijbewijsDocNr;
        private string startDatum;
        private string eindDatum;
        private string status;
        private string aardeVanReis;
        private string versteBestemming;

        public void createAanvraag()
        {
        }

        public void updateStatus()
        {
        }
    }
}
