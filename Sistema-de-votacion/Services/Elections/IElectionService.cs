using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Services.Elections
{
    public interface IElectionService
    {
        Task<Election> InsertElectionAsync(Election election, List<Candidate> electionCandidates, List<Position> electionPostion);
        Task<IQueryable<Election>> GetElectionsAsync();
        Task<Election> GetElectionByIdAsync(int? id);
        Task<Election> UpdateElectionAsync(Election election);
        Task<Election> DeleteElectionAsync(Election election);
        Task<bool> VerifyCitizenVoteAsync(string CitizenDNI);
        Task<bool> VerifyElectionOpenAsync();
        Task<List<Result>> GetElectionResultsByIdAsync(int electionId);
        Task<IQueryable<Election>> GetElectionByConditionAsync(Expression<Func<Election, bool>> predicate);
        Task<IEnumerable<Result>> InsertElectionResulAsync(IEnumerable<Result> results);
        Task<ElectionCitizen> InsertElectionCitizenVote(ElectionCitizen electionCitizen);
    }
}
