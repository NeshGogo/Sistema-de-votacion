using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Services.Candidates.Positions
{
    public interface IPositionService
    {
       Task<Position> InsertPosition(Position position);
        Task<IQueryable<Position>> GetPositions();

        Task<Position> GetPositionById(int Id);
        Task<Position> UdatePosition(Position position);
        Task<Position> DeletePosition(Position position);
    }
}
