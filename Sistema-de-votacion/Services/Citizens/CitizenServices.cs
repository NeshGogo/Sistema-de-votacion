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

        public Citizen DeleteCitizen(Citizen citizen)
        {
           return _citizenRepository.Delete(citizen);
        }

        public IQueryable<Citizen> GetCitizen()
        {
            return _citizenRepository.GetAll();
        }

        public Citizen GetCitizenById(int? id)
        {
            if (id.HasValue)
            {
                return _citizenRepository.GetById(id);
            }
            else
            {
                return null; 
            }            
        }

        public Citizen InsertCitizen(Citizen citizen)
        {
            return _citizenRepository.Insert(citizen);
        }

        public Citizen UdateCitizen(Citizen citizen)
        {
            return _citizenRepository.Update(citizen);
        }
    }
}
