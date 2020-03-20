using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_votacion.Models
{
    public partial class Position
    {
        public Position()
        {
            Candidate = new HashSet<Candidate>();
            ElectionPosition = new HashSet<ElectionPosition>();
        }

        public int Id { get; set; }
        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="Este campo es requerido")]
        [MaxLength(50, ErrorMessage = "La longitud maxima es de 50 caracteres.")]
        public string Name { get; set; }
        [Display(Name = "Descriccion")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [MaxLength(80, ErrorMessage ="La longitud maxima es de 80 caracteres.")]
        public string Description { get; set; }
        [Display(Name = "Activo")]
        public bool IsActive { get; set; }

        public virtual ICollection<Candidate> Candidate { get; set; }
        public virtual ICollection<ElectionPosition> ElectionPosition { get; set; }
    }
}
