using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data.Citizens
{
    public class CitizenRepository : SQLRepository<Citizen>, ICitizenRepository
    {
        public CitizenRepository(ElectionDBContext context): base(context)
        {

        }
    }
}
