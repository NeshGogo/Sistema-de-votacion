using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Services.PoliticParties
{
    public interface IPoliticPartyService
    {
        PoliticParty InsertPoliticParty(PoliticParty politicParty);
        IQueryable<PoliticParty> GetPoliticParties();

        PoliticParty GetPoliticPartyById(int Id);
        PoliticParty UdatePoliticParty(PoliticParty politicParty);
        PoliticParty DeletePoliticParty(PoliticParty politicParty);
    }
}
