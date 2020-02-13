using System;
using System.Collections.Generic;

namespace Sistema_de_votacion.Models
{
    public partial class Position
    {
        public Position()
        {
            Candidate = new HashSet<Candidate>();
            ElectionPosition = new HashSet<ElectionPosition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Candidate> Candidate { get; set; }
        public virtual ICollection<ElectionPosition> ElectionPosition { get; set; }
    }
}
