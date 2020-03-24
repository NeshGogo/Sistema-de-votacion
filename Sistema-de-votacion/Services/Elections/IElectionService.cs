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
        Task<Election> InsertElection(Election election, List<int> ElectionCandidates, 
            List<int> ElectionCitizens, List<int> ElectionPositions, List<int> ElectionPoliticParties);
        Task<IQueryable<Election>> GetElections();
        Task<Election> GetElectionById(int? id);
        Task<Election> UpdateElection(Election election);
        Task<Election> DeleteElection(Election election);
    }
}
