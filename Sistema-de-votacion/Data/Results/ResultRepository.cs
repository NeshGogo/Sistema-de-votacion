using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data.Results
{
    public class ResultRepository:SQLRepository<Result>, IResultRepository
    {
        public ResultRepository(ElectionDBContext context) : base(context)
        {

        }
    }
}
