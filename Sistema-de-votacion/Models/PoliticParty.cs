using System;
using System.Collections.Generic;

namespace Sistema_de_votacion.Models
{
    public partial class PoliticParty
    {
        public PoliticParty()
        {
            Candidate = new HashSet<Candidate>();
            ElectionPoliticParty = new HashSet<ElectionPoliticParty>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PartyLogoPath { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Candidate> Candidate { get; set; }
        public virtual ICollection<ElectionPoliticParty> ElectionPoliticParty { get; set; }
    }
}
