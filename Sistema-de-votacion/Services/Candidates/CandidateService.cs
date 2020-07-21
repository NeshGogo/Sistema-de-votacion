  using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_de_votacion.Data.Candidates;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Services.Candidates
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            this._candidateRepository = candidateRepository;
        }
        public Candidate InsertCandidate(Candidate candidate)
        {
            return _candidateRepository.Insert(candidate);
        }
        public Candidate GetCandidateById(int? Id)
        {
            return _candidateRepository.GetAll().Where(c => c.Id == Id).Include(c => c.Position).FirstOrDefault();
        }

        public IQueryable<Candidate> GetCandidates()
        {
            return _candidateRepository.GetAll();
        }
        public Candidate UpdateCandidate(Candidate candidate)
        {
            return _candidateRepository.Update(candidate);
        }
        public Candidate DeleteCandidate(Candidate candidate)
        {
            candidate.IsActive = false;
            return _candidateRepository.Update(candidate);
        }

    }
}
