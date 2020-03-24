using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data.ElectionCitizens
{
    public class ElectionCitizenRepository : SQLRepository<ElectionCitizen>, IElectionCitizenRepository
    {
        public ElectionCitizenRepository(ElectionDBContext context) : base(context)
        {
        }
    }
}
