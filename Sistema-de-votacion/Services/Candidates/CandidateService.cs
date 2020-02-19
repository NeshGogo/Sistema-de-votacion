﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return _candidateRepository.GetById(Id);
        }

        public IQueryable<Candidate> GetCandidates()
        {
            return _candidateRepository.GetAll();
        }
        public Candidate UdateCandidate(Candidate candidate)
        {
            return _candidateRepository.Update(candidate);
        }
        public Candidate DeleteCandidate(Candidate candidate)
        {
            return _candidateRepository.Delete(candidate);
        }

    }
}