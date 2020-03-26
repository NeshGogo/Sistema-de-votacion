using System;
using System.Collections.Generic;

namespace Sistema_de_votacion.Models
{
    public class ElectionPosition
    {
        public int Id { get; set; }
        public int ElectionId { get; set; }
        public int PositionId { get; set; }

        public virtual Election Election { get; set; }
        public virtual Position Position { get; set; }
    }
}
