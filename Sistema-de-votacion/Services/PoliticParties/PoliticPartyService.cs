using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistema_de_votacion.Data.PoliticParties;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Services.PoliticParties
{
    public class PoliticPartyService : IPoliticPartyService
    {
        private readonly IPoliticPartyRepository _politicPartyRepository;

        public PoliticPartyService(IPoliticPartyRepository politicPartyRepository)
        {
            this._politicPartyRepository = politicPartyRepository;
        }
        public PoliticParty InsertPoliticParty(PoliticParty politicParty)
        {
            return _politicPartyRepository.Insert(politicParty);
        }
        public PoliticParty GetPoliticPartyById(int Id)
        {
            return _politicPartyRepository.GetById(Id);
        }
        public IQueryable<PoliticParty> GetPoliticParties()
        {
            return _politicPartyRepository.GetAll();
        }
        public PoliticParty UdatePoliticParty(PoliticParty politicParty)
        {
            return _politicPartyRepository.Update(politicParty);
        }
        public PoliticParty DeletePoliticParty(PoliticParty politicParty)
        {
            return _politicPartyRepository.Delete(politicParty);
        }

    }
}
