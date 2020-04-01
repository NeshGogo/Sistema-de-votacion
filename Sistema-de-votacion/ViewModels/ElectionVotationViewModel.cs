using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.ViewModels
{
    public class ElectionVotationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrentPositionName { get; set; }
        public int CurrentPositionId { get; set; }
        public List<Candidate> Candidates { get; set; }
        public List<Position> Postions { get; set; }

    }
}
