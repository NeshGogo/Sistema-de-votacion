using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.ViewModels
{
    public class ElectionCreateViewModel
    {
        [Required(ErrorMessage ="Este campo es requerido.")]
        [Display(Name ="Nombre")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Posiciones")]
        public List<int> ElectionPosition { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Candidatos")]
        public  List<int> ElectionCadidate { get; set; }



    }
}
