using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Services.Candidates
{
    public interface ICandidateService
    {
        Candidate InsertCandidate(Candidate candidate);
        IQueryable<Candidate> GetCandidates();

        Candidate GetCandidateById(int? Id);
        Candidate UdateCandidate(Candidate candidate);
        Candidate DeleteCandidate(Candidate candidate);
    }
}
