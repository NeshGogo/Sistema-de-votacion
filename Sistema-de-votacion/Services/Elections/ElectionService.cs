using Sistema_de_votacion.Data.ElectionCandidates;
using Sistema_de_votacion.Data.ElectionCitizens;
using Sistema_de_votacion.Data.ElectionPoliticParties;
using Sistema_de_votacion.Data.ElectionPositions;
using Sistema_de_votacion.Data.Elections;
using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Services.Elections
{
    public class ElectionService : IElectionService
    {
        private readonly IElectionRepository _electionRepository;
        private readonly IElectionCandidateRepository _electionCandidateRepository;
        private readonly IElectionCitizenRepository _electionCitizenRepository;
        private readonly IElectionPoliticPartyRepository _electionPoliticPartyRepository;
        private readonly IElectionPositionRepository _electionPositionRepository;

        public ElectionService(IElectionRepository electionRepository, IElectionCandidateRepository electionCandidateRepository, 
            IElectionCitizenRepository electionCitizenRepository, IElectionPoliticPartyRepository electionPoliticPartyRepository, 
            IElectionPositionRepository electionPositionRepository)
        {
           
            _electionRepository = electionRepository;
            _electionCandidateRepository = electionCandidateRepository;
            _electionCitizenRepository = electionCitizenRepository;
            _electionPoliticPartyRepository = electionPoliticPartyRepository;
            _electionPositionRepository = electionPositionRepository;
        }
        public async Task<Election> DeleteElection(Election election)
        {
            election.IsActive = false;
            return await Task.FromResult(_electionRepository.Update(election));
        }

        public async  Task<Election> GetElectionById(int? id)
        {
            return await Task.FromResult(_electionRepository.GetById(id.Value));
        }

        public async  Task<IQueryable<Election>> GetElections()
        {
            return await Task.FromResult(_electionRepository.GetAll());
        }

        public async Task<Election> InsertElection(Election election, List<int> electionCandidates, List<int> electionCitizens, List<int> electionPositions, List<int> electionPoliticParties)
        {
            election.Date = DateTime.UtcNow;
            election.IsActive = true;
            Election result = await Task.FromResult(_electionRepository.Insert(election));
            if (result != null)
            {
                List<ElectionCadidate> cadidates = electionCandidates.Select(e => new ElectionCadidate { CandidateId = e, ElectionId = result.Id }).ToList();
                List<ElectionPosition> positions = electionPositions.Select(e => new ElectionPosition { PositionId = e, ElectionId = result.Id }).ToList();
                List<ElectionCitizen> citizens = electionCitizens.Select(e => new ElectionCitizen { CitizenId = e, ElectionId = result.Id }).ToList();
                List<ElectionPoliticParty> politicParties = electionPoliticParties.Select(e => new ElectionPoliticParty { PoliticPartyId = e, ElectionId = result.Id }).ToList();

                _electionCandidateRepository.Insert(cadidates);
                _electionCitizenRepository.Insert(citizens);
                _electionPoliticPartyRepository.Insert(politicParties);
                _electionPositionRepository.Insert(positions);
            }
            return result;
        }

        public async Task<Election> UpdateElection(Election election)
        {
            return await Task.FromResult(_electionRepository.Update(election));
        }

        public async Task<bool> VerifyCitizenVote(int citizenId)
        {
           return await Task.FromResult( _electionCitizenRepository.GetAll().Any(ec => ec.CitizenId == citizenId) );       
            
        }
    }
}
