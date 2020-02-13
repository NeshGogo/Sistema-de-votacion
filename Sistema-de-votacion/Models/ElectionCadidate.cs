using System;
using System.Collections.Generic;

namespace Sistema_de_votacion.Models
{
    public partial class ElectionCadidate
    {
        public int Id { get; set; }
        public int ElectionId { get; set; }
        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual Election Election { get; set; }
    }
}
