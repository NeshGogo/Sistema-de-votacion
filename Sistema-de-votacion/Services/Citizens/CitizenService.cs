using Sistema_de_votacion.Data.Citizens;
using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Services.Citizens
{
    public class CitizenService: ICitizenService
    {
        private readonly ICitizenRepository _citizenRepository;
        public CitizenService(ICitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
        }
        public async Task<IQueryable<Citizen>> GetCitizenByConditionAsync(Expression<Func<Citizen, bool>> predicate)
        {
            return await Task.FromResult( _citizenRepository.GetAll().Where(predicate) );
        }

        public async Task<Citizen> DeleteCitizenAsync(Citizen citizen)
        {
            citizen.IsActive = false;
           return await Task.FromResult( _citizenRepository.Update(citizen));
        }

        public async Task<IQueryable<Citizen>> GetCitizensAsync()
        {
            return await Task.FromResult( _citizenRepository.GetAll());
        }

        public async Task<Citizen> GetCitizenByIdAsync(int? id)
        {
            if (id.HasValue)
            {
                return await Task.FromResult( _citizenRepository.GetById(id) );
            }
            else
            {
                return null; 
            }            
        }

        public async Task<Citizen> InsertCitizenAsync(Citizen citizen)
        {
            citizen.IsActive = true;
            return await Task.FromResult( _citizenRepository.Insert(citizen) );
        }

        public async Task<Citizen> UdateCitizenAsync(Citizen citizen)
        {
            return await Task.FromResult( _citizenRepository.Update(citizen) );
        }

        public async  Task<bool> VerifyExist(string DNI)
        {
           return  await Task.FromResult( _citizenRepository.GetAll().Any(c => c.Dni == DNI && c.IsActive == true));
        }
    }
}
