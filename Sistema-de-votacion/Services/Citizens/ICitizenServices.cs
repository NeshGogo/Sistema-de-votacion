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
        Task<Citizen> InsertCitizenAsync(Citizen citizen);
        Task<IQueryable<Citizen>> GetCitizensAsync();

        Task<Citizen> GetCitizenByIdAsync(int? id);
        Task<Citizen> UdateCitizenAsync(Citizen citizen);
        Task<Citizen> DeleteCitizenAsync(Citizen citizen);
        Task<IQueryable<Citizen>> GetCitizenByConditionAsync(Expression<Func<Citizen, bool>> predicate);
        Task<bool> VerifyExistAsync(string DNI);
    }
}
