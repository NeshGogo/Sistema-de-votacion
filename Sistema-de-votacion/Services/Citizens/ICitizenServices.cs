using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Services.Citizens
{
    public interface ICitizenServices
    {
        Citizen InsertCitizen(Citizen citizen);
        IQueryable<Citizen> GetCitizen();

        Citizen GetCitizenById(int? id);
        Citizen UdateCitizen(Citizen citizen);
        Citizen DeleteCitizen(Citizen citizen);
    }
}
