using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sistema_de_votacion.Data.Candidates;
using Sistema_de_votacion.Data.Positions;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Services.Candidates.Positions
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly ICandidateRepository _candidateRepository;

        public PositionService(IPositionRepository positionRepository, ICandidateRepository candidateRepository)
        {
            this._positionRepository = positionRepository;
            this._candidateRepository = candidateRepository;
        }
        public async Task<Position> InsertPositionAsync(Position position)
        {
            return await Task.FromResult(_positionRepository.Insert(position));
        }
        public async Task<Position> GetPositionByIdAsync(int Id)
        {
            return await Task.FromResult(_positionRepository.GetById(Id));
        }

        public async Task<IQueryable<Position>> GetPositionsAsync()
        {
            return await Task.FromResult(_positionRepository.GetAll());
        }
        public async Task<Position> UdatePositionAsync(Position position)
        {
            return await Task.FromResult( _positionRepository.Update(position));
        }
        public async Task<Position> DeletePositionAsync(Position position)
        {
            position.IsActive = false;
            var candidates = _candidateRepository.GetAll().Where(c => c.PositionId == position.Id).Select( c => new Candidate{  Id = c.Id, IsActive = false, Name = c.Name, LastName = c.LastName, PoliticPartyId =c.PoliticPartyId, PositionId = c.PositionId, ProfilePhothoPath = c.ProfilePhothoPath }).ToList();
            _candidateRepository.Update(candidates);            
            return await Task.FromResult( _positionRepository.Update(position));
        }

        public async  Task<IEnumerable<Position>> GetPositionsByConditionAsync(Expression<Func<Position, bool>> predicate)
        {
            return await Task.FromResult(_positionRepository.GetAll().Where(predicate));
        }
    }
}
