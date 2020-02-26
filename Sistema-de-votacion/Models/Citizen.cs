using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_votacion.Models
{
    public partial class Citizen
    {
        public Citizen()
        {
            ElectionCitizen = new HashSet<ElectionCitizen>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Este campo es requerido")]
        [Display(Name ="Cedula de Identidad")]
        [StringLength(11, ErrorMessage ="Detener un maximo y un minimo de 11 caracteres.")]
        public string Dni { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(80, ErrorMessage = "Detener un maximo de 80 caracteres.")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(80, ErrorMessage = "Detener un maximo de 80 caracteres.")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Correo Electronico")]
        [MaxLength(150, ErrorMessage = "Detener un maximo de 150 caracteres.")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Debe introducir un correo electronico")]
        public string Email { get; set; }
        [Display(Name = "Activio")]
        public bool IsActive { get; set; }

        public virtual ICollection<ElectionCitizen> ElectionCitizen { get; set; }
    }
}
