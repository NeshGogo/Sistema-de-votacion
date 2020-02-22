using Sistema_de_votacion.Data.Citizens;
using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Services.Citizens
{
    public class CitizenServices: ICitizenServices
    {
        private readonly ICitizenRepository _citizenRepository;
        public CitizenServices(ICitizenRepository citizenRepository)
        {
            _citizenRepository = citizenRepository;
        }

        public async Task<Citizen> DeleteCitizen(Citizen citizen)
        {
           return await Task.FromResult( _citizenRepository.Delete(citizen));
        }

        public async Task<IQueryable<Citizen>> GetCitizens()
        {
            return await Task.FromResult( _citizenRepository.GetAll());
        }

        public async Task<Citizen> GetCitizenById(int? id)
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

        public async Task<Citizen> InsertCitizen(Citizen citizen)
        {
            return await Task.FromResult( _citizenRepository.Insert(citizen) );
        }

        public async Task<Citizen> UdateCitizen(Citizen citizen)
        {
            return await Task.FromResult( _citizenRepository.Update(citizen) );
        }
    }
}
