using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data.Positions
{
    public class PositionRepository:SQLRepository<Position>, IPositionRepository
    {
        public PositionRepository(ElectionDBContext context):base(context)
        {

        }
    }
}
