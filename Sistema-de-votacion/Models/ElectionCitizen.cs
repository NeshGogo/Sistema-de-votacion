using System;
using System.Collections.Generic;

namespace Sistema_de_votacion.Models
{
    public partial class ElectionCitizen
    {
        public int Id { get; set; }
        public int ElectionId { get; set; }
        public int CitizenId { get; set; }

        public virtual Citizen Citizen { get; set; }
        public virtual Election Election { get; set; }
    }
}
