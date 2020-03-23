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

        public ElectionService(IElectionRepository electionRepository)
        {
           
            _electionRepository = electionRepository;
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

        public async Task<Election> InsertElection(Election election)
        {
            return await Task.FromResult(_electionRepository.Insert(election));
        }

        public async Task<Election> UpdateElection(Election election)
        {
            return await Task.FromResult(_electionRepository.Update(election));
        }
    }
}
