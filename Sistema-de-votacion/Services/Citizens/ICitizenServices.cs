using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Services.Citizens
{
    public interface ICitizenService
    {
        Task<Citizen> InsertCitizen(Citizen citizen);
        Task<IQueryable<Citizen>> GetCitizens();

        Task<Citizen> GetCitizenById(int? id);
        Task<Citizen> UdateCitizen(Citizen citizen);
        Task<Citizen> DeleteCitizen(Citizen citizen);
        Task<IQueryable<Citizen>> GetCitizenByCondition(Expression<Func<Citizen, bool>> predicate);
    }
}
