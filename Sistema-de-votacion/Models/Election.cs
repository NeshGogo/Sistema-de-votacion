using System;
using System.Collections.Generic;

namespace Sistema_de_votacion.Models
{
    public partial class Election
    {
        public Election()
        {
            ElectionCadidate = new HashSet<ElectionCadidate>();
            ElectionCitizen = new HashSet<ElectionCitizen>();
            ElectionPoliticParty = new HashSet<ElectionPoliticParty>();
            ElectionPosition = new HashSet<ElectionPosition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ElectionCadidate> ElectionCadidate { get; set; }
        public virtual ICollection<ElectionCitizen> ElectionCitizen { get; set; }
        public virtual ICollection<ElectionPoliticParty> ElectionPoliticParty { get; set; }
        public virtual ICollection<ElectionPosition> ElectionPosition { get; set; }
    }
}
