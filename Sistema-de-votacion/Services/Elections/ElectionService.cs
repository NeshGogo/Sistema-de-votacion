using Sistema_de_votacion.Data.ElectionCandidates;
using Sistema_de_votacion.Data.ElectionCitizens;
using Sistema_de_votacion.Data.ElectionPoliticParties;
using Sistema_de_votacion.Data.ElectionPositions;
using Sistema_de_votacion.Data.Elections;
using Sistema_de_votacion.Data.Results;
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
        private readonly IResultRepository _resultRepository;

        public ElectionService(IElectionRepository electionRepository, IElectionCandidateRepository electionCandidateRepository, 
            IElectionCitizenRepository electionCitizenRepository, IElectionPoliticPartyRepository electionPoliticPartyRepository, 
            IElectionPositionRepository electionPositionRepository, IResultRepository resultRepository)
        {
           
            _electionRepository = electionRepository;
            _electionCandidateRepository = electionCandidateRepository;
            _electionCitizenRepository = electionCitizenRepository;
            _electionPoliticPartyRepository = electionPoliticPartyRepository;
            _electionPositionRepository = electionPositionRepository;
            _resultRepository = resultRepository;
        }
        public async Task<Election> DeleteElectionAsync(Election election)
        {
            election.IsActive = false;
            return await Task.FromResult(_electionRepository.Update(election));
        }

        public async Task<IQueryable<Result>> GetElectionResultsByIdAsync(int electionId)
        {
           return await Task.FromResult(_resultRepository.GetAll().Where(e => e.ElectionId == electionId));
        }

        public async  Task<Election> GetElectionByIdAsync(int? id)
        {
            return await Task.FromResult(_electionRepository.GetById(id.Value));
        }

        public async  Task<IQueryable<Election>> GetElectionsAsync()
        {
            return await Task.FromResult(_electionRepository.GetAll());
        }

        public async Task<Election> InsertElectionAsync(Election election, List<Candidate> electionCandidates)
        {
            election.Date = DateTime.UtcNow;
            election.IsActive = true;
            Election result = await Task.FromResult(_electionRepository.Insert(election));
            if (result != null)
            {
                List<ElectionCadidate> cadidates = electionCandidates.Select(e => new ElectionCadidate { CandidateId = e.Id, ElectionId = result.Id }).ToList();                

                _electionCandidateRepository.Insert(cadidates);
            }
            return result;
        }

        public async Task<Election> UpdateElectionAsync(Election election)
        {
            return await Task.FromResult(_electionRepository.Update(election));
        }

        public async Task<bool> VerifyCitizenVoteAsync(int citizenId)
        {
           return await Task.FromResult( _electionCitizenRepository.GetAll().Any(ec => ec.CitizenId == citizenId) );       
            
        }

        public async Task<bool> VerifyElectionOpenAsync()
        { 
            return await Task.FromResult(_electionRepository.GetAll().Any(e => e.IsActive == true));
        }
    }
}
