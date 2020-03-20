using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_votacion.DTO
{
    public class LoginDTO
    {
        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage ="Este campo es requerido")]
        [StringLength(50, ErrorMessage ="La longitud maxima de caracteres es de 50")]
        public string UserName { get; set; }

        [Display(Name ="Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
