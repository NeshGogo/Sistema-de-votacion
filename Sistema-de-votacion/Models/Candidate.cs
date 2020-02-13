using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_votacion.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            ElectionCadidate = new HashSet<ElectionCadidate>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage ="Este campo obligatorio")]
        [Display(Name ="Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este campo obligatorio")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Este campo obligatorio")]
        [Display(Name = "Partido")]
        public int PoliticPartyId { get; set; }
        [Required(ErrorMessage = "Este campo obligatorio")]
        [Display(Name = "Posición")]
        public int PositionId { get; set; }
        [Required(ErrorMessage = "Este campo obligatorio")]
        [Display(Name = "Foto de perfil")]
        public string ProfilePhothoPath { get; set; }
        [Required(ErrorMessage = "Este campo obligatorio")]
        [Display(Name = "Esta Activo")]
        public bool IsActive { get; set; }

        public virtual PoliticParty PoliticParty { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<ElectionCadidate> ElectionCadidate { get; set; }
    }
}
