using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistema_de_votacion.Data.Positions;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Services.Candidates.Positions
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            this._positionRepository = positionRepository;
        }
        public async Task<Position> InsertPosition(Position position)
        {
            return await Task.FromResult(_positionRepository.Insert(position));
        }
        public async Task<Position> GetPositionById(int Id)
        {
            return await Task.FromResult(_positionRepository.GetById(Id));
        }

        public async Task<IQueryable<Position>> GetPositions()
        {
            return await Task.FromResult(_positionRepository.GetAll());
        }
        public async Task<Position> UdatePosition(Position position)
        {
            return await Task.FromResult( _positionRepository.Update(position));
        }
        public async Task<Position> DeletePosition(Position position)
        {
            position.IsActive = false;
            return await Task.FromResult( _positionRepository.Update(position));
        }

    }
}
