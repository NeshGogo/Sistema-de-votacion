using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Services.Candidates.Positions
{
    public interface IPositionService
    {
        Position InsertPosition(Position position);
        IQueryable<Position> GetPositions();

        Position GetPositionById(int Id);
        Position UdatePosition(Position position);
        Position DeletePosition(Position position);
    }
}
