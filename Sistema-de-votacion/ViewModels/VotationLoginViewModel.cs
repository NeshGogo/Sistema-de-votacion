using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.ViewModels
{
    public class VotationLoginViewModel
    {
        [MaxLength(11,ErrorMessage ="Debe tener 11 digitos.")]
        [MinLength(11, ErrorMessage = "Debe tener 11 digitos.")]
        [Required(ErrorMessage ="Este campo es requerido")]
        public string DNI { get; set; }
    }
}
