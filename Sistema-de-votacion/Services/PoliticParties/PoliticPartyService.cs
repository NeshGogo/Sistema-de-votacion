using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistema_de_votacion.Data.Candidates;
using Sistema_de_votacion.Data.PoliticParties;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Services.PoliticParties
{
    public class PoliticPartyService : IPoliticPartyService
    {
        private readonly IPoliticPartyRepository _politicPartyRepository;
        private readonly ICandidateRepository _candidateRepository;

        public PoliticPartyService(IPoliticPartyRepository politicPartyRepository, ICandidateRepository candidateRepository)
        {
            this._politicPartyRepository = politicPartyRepository;
            this._candidateRepository = candidateRepository;
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
            politicParty.IsActive = false;
             _politicPartyRepository.Update(politicParty);
            var candidates = _candidateRepository.GetAll().Where(c => c.PoliticPartyId == politicParty.Id)
                .Select(c => new Candidate
                {
                    Id = c.Id,
                    IsActive = false,
                    Name = c.Name,
                    LastName = c.LastName,
                    PoliticPartyId = c.PoliticPartyId,
                    PositionId = c.PositionId,
                    ProfilePhothoPath = c.ProfilePhothoPath
                }).ToList();  
            
           
            _candidateRepository.Update(candidates);
            return politicParty;
        }

    }
}
