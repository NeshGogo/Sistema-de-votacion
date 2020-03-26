using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Services.Candidates.Positions
{
    public interface IPositionService
    {
       Task<Position> InsertPositionAsync(Position position);
        Task<IQueryable<Position>> GetPositionsAsync();
        Task<Position> GetPositionByIdAsync(int Id);
        Task<Position> UdatePositionAsync(Position position);
        Task<Position> DeletePositionAsync(Position position);
        Task<IEnumerable<Position>> GetPositionsByConditionAsync(Expression<Func<Position, bool>> predicate);
    }
}
