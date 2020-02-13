using System;
using System.Collections.Generic;

namespace Sistema_de_votacion.Models
{
    public partial class Citizen
    {
        public Citizen()
        {
            ElectionCitizen = new HashSet<ElectionCitizen>();
        }

        public int Id { get; set; }
        public string Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ElectionCitizen> ElectionCitizen { get; set; }
    }
}
