﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Services.Elections
{
    public interface IElectionService
    {
        Task<Election> InsertElectionAsync(Election election, List<int> ElectionCandidates, 
            List<int> ElectionCitizens, List<int> ElectionPositions, List<int> ElectionPoliticParties);
        Task<IQueryable<Election>> GetElectionsAsync();
        Task<Election> GetElectionByIdAsync(int? id);
        Task<Election> UpdateElectionAsync(Election election);
        Task<Election> DeleteElectionAsync(Election election);
        Task<bool> VerifyCitizenVoteAsync(int CitizenId);
        Task<bool> VerifyElectionOpenAsync();
    }
}
