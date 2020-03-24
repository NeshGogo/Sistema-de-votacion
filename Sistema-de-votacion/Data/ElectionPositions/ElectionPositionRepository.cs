using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data.ElectionPositions
{
    public class ElectionPositionRepository : SQLRepository<ElectionPosition>, IElectionPositionRepository
    {
        public ElectionPositionRepository(ElectionDBContext context) : base(context)
        {
        }
    }
}
