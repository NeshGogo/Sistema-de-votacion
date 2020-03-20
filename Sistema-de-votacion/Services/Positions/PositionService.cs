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
        public Position InsertPosition(Position position)
        {
            return _positionRepository.Insert(position);
        }
        public Position GetPositionById(int Id)
        {
            return _positionRepository.GetById(Id);
        }

        public IQueryable<Position> GetPositions()
        {
            return _positionRepository.GetAll();
        }
        public Position UdatePosition(Position position)
        {
            return _positionRepository.Update(position);
        }
        public Position DeletePosition(Position position)
        {
            position.IsActive = false;
            return _positionRepository.Update(position);
        }

    }
}
