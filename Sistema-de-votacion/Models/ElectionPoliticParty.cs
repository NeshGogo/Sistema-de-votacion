using System;
using System.Collections.Generic;

namespace Sistema_de_votacion.Models
{
    public partial class ElectionPoliticParty
    {
        public int Id { get; set; }
        public int ElectionId { get; set; }
        public int PoliticPartyId { get; set; }

        public virtual Election Election { get; set; }
        public virtual PoliticParty PoliticParty { get; set; }
    }
}
