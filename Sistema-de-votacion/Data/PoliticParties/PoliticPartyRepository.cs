﻿using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Data.PoliticParties
{
    public class PoliticPartyRepository : SQLRepository<PoliticParty>, IPoliticPartyRepository
    {
        public PoliticPartyRepository(ElectionDBContext context) : base(context)
        {

        }
    }
}
