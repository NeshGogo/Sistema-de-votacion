using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data.ElectionPoliticParties
{
    public class ElectionPoliticPartyRepository : SQLRepository<ElectionPoliticParty>, IElectionPoliticPartyRepository
    {
        public ElectionPoliticPartyRepository(ElectionDBContext context) : base(context)
        {
        }
    }
}
