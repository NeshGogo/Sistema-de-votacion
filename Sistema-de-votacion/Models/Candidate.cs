using System;
using System.Collections.Generic;

namespace Sistema_de_votacion.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            ElectionCadidate = new HashSet<ElectionCadidate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int PoliticPartyId { get; set; }
        public int PositionId { get; set; }
        public string ProfilePhothoPath { get; set; }
        public bool IsActive { get; set; }

        public virtual PoliticParty PoliticParty { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<ElectionCadidate> ElectionCadidate { get; set; }
    }
}
