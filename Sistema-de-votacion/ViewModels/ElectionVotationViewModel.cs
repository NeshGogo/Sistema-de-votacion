using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.ViewModels
{
    public class ElectionVotationViewModel
    {
        public int ElectionId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public int PositionIndex { get; set; }
        public Position Position { get; set; }
        public List<Candidate> Candidates { get; set; }
        public int CitizenId { get; set; }

        public virtual ICollection<ElectionCadidate> ElectionCadidate { get; set; }
        public virtual ICollection<ElectionCitizen> ElectionCitizen { get; set; }
        public virtual ICollection<ElectionPoliticParty> ElectionPoliticParty { get; set; }
        public virtual ICollection<ElectionPosition> ElectionPosition { get; set; }
    }
}
