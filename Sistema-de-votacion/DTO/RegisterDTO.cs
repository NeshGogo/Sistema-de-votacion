using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.DTO
{
    public class RegisterDTO
    {
        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(50, ErrorMessage = "La longitud maxima de caracteres es de 50.")]
        public string UserName { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(50, ErrorMessage = "La longitud maxima de caracteres es de 50.")]
        public string Nombre { get; set; }

        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Debe introducir un correo electronico valido.")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Las contraseñas no son iguales.")]
        public string PasswordConfirm { get; set; }

    }
}
