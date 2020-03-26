using Microsoft.AspNetCore.Http;
using Sistema_de_votacion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.ViewModels
{
    public class PoliticPartyCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo obligatorio")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este campo obligatorio")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        //[Required(ErrorMessage = "Este campo obligatorio")]
        [Display(Name = "Foto de Perfil")]
        public IFormFile PartyLogoPath { get; set; }
        [Required(ErrorMessage = "Este campo obligatorio")]
        [Display(Name = "Es activo")]
        public bool IsActive { get; set; }

        public virtual ICollection<Candidate> Candidate { get; set; }
        public virtual ICollection<ElectionPoliticParty> ElectionPoliticParty { get; set; }
        public string Photo { get; set; }
    }
}
