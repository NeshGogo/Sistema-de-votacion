using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Data.Elections
{
    public class ElectionRepository: SQLRepository<Election>, IElectionRepository
    {

        public ElectionRepository(ElectionDBContext context): base(context)
        {

        }
    }
}
