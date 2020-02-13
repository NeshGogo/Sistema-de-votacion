using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data.Candidates
{
    public class CandidateRepository : SQLRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(ElectionDBContext context) : base(context)
        {

        }
    }
}
