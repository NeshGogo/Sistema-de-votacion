using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data.ElectionCandidates
{
    public class ElectionCandidateRepository : SQLRepository<ElectionCadidate>, IElectionCandidateRepository
    {
        public ElectionCandidateRepository(ElectionDBContext context) : base(context)
        {
        }
    }
}
