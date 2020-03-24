using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.ViewModels
{
    public class ElectionCreateViewModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public  List<int> ElectionCadidate { get; set; }
        public List<int> ElectionCitizen { get; set; }
        public List<int> ElectionPoliticParty { get; set; }
        public List<int> ElectionPosition { get; set; }
    }
}
