using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent_it.React.Server.Models.RentIt
{
    public class Voertuig
    {
        [Key]
        public int VoertuigId { get; set; }

        [Required]
        [StringLength(50)]
        public string Soort { get; set; } // Auto, Camper, Caravan

        [Required]
        [StringLength(50)]
        public string Merk { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(30)]
        public string Kleur { get; set; }

        [Required]
        [StringLength(20)]
        public string Kenteken { get; set; }

        public int Aanschafjaar { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrijsPerDag { get; set; } // Verhuurprijs per dag

        public bool Beschikbaar { get; set; } = true;

        // Navigatie-eigenschap
        public ICollection<VerhuurAanvraag> VerhuurAanvragen { get; set; }

        // Methode: Beschikbaarheid controleren
        public bool IsBeschikbaar(DateTime startDatum, DateTime eindDatum)
        {
            if (VerhuurAanvragen == null) return true;

            foreach (var aanvraag in VerhuurAanvragen)
            {
                if ((startDatum <= aanvraag.EindDatum && startDatum >= aanvraag.StartDatum) ||
                    (eindDatum <= aanvraag.EindDatum && eindDatum >= aanvraag.StartDatum))
                {
                    return false; // Niet beschikbaar
                }
            }
            return true; // Beschikbaar
        }
    }
}
