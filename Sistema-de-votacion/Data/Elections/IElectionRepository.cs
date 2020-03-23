using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Data.Elections
{
    public interface IElectionRepository: IRepository<Election>
    {
    }
}
