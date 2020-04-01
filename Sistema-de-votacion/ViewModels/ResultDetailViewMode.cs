using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.ViewModels
{
    public class ResultDetailViewMode
    {
        [Display(Name ="Nombre")]
        public string Name { get; set; }
        public List<Candidate> Candidates { get; set; }
        public List<IGrouping<string,Candidate>> Votes { get; set; }
    }
}
