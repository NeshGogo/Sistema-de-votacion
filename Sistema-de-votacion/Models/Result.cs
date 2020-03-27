using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int ElectionId { get; set; }
        public int CandidateId { get; set; }
        public int CitizenId { get; set; }
        public Election Election { get; set; }
        public Candidate Candidate { get; set; }
        public Citizen Citizen { get; set; }
    }
}
